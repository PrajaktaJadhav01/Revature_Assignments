using Microsoft.AspNetCore.Mvc;
using WebApiProject.Data;
using WebApiProject.Models;
using WebApiProject.DTOs;
using AutoMapper;

namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CustomerController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/customer
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _context.Customers.ToList();
            var customerDTO = _mapper.Map<List<CustomerDTO>>(customers);

            return Ok(customerDTO);
        }

        // POST
        [HttpPost]
        public IActionResult AddCustomer(CustomerDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Ok(customer);
        }
    }
}