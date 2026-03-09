using Microsoft.AspNetCore.Mvc;
using WebApiProject.Data;
using WebApiProject.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IDistributedCache _cache;

        public CustomerController(AppDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: api/customer
        [HttpGet]
        public IActionResult GetCustomers()
        {
            string cacheKey = "customerList";

            var cachedCustomers = _cache.GetString(cacheKey);

            if (!string.IsNullOrEmpty(cachedCustomers))
            {
                var customersFromCache = JsonSerializer.Deserialize<List<Customer>>(cachedCustomers);
                return Ok(customersFromCache);
            }

            var customers = _context.Customers.ToList();

            var serializedCustomers = JsonSerializer.Serialize(customers);

            _cache.SetString(cacheKey, serializedCustomers);

            return Ok(customers);
        }

        // GET: api/customer/1
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST: api/customer
        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Ok(customer);
        }

        // PUT: api/customer/1
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer customer)
        {
            var existingCustomer = _context.Customers.Find(id);

            if (existingCustomer == null)
                return NotFound();

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;

            _context.SaveChanges();

            return Ok(existingCustomer);
        }

        // DELETE: api/customer/1
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return Ok("Customer deleted successfully");
        }
    }
}