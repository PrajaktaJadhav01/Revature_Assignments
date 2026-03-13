using Microsoft.AspNetCore.Mvc;
using WebApiProject.Data;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/customer
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _context.Customers.ToList();

            Console.WriteLine("Customers fetched from database:");

            foreach (var c in customers)
            {
                Console.WriteLine($"ID: {c.Id} | Name: {c.Name} | Email: {c.Email}");
            }

            return Ok(customers);
        }

        // GET: api/customer/1
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound();

            Console.WriteLine($"Customer fetched: {customer.Id} - {customer.Name} - {customer.Email}");

            return Ok(customer);
        }

        // POST: api/customer
        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            Console.WriteLine($"Customer Added: {customer.Id} - {customer.Name} - {customer.Email}");

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

            Console.WriteLine($"Customer Updated: {existingCustomer.Id} - {existingCustomer.Name}");

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

            Console.WriteLine($"Customer Deleted: {customer.Id} - {customer.Name}");

            return Ok("Customer deleted successfully");
        }
    }
}