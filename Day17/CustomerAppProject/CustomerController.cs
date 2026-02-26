using Microsoft.AspNetCore.Mvc;

namespace CustomerAppProject.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _service;

        public CustomerController(CustomerService service)
        {
            _service = service;
        }

        // GET with FromQuery
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers([FromQuery] string? name)
        {
            var customers = await _service.GetAllCustomersAsync(name);
            return Ok(customers);
        }

        // GET by ID (FromRoute)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {
            var customer = await _service.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST (FromBody)
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            await _service.AddCustomerAsync(customer);
            return Ok("Customer Created");
        }

        // PUT (FromBody)
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            await _service.UpdateCustomerAsync(customer);
            return Ok("Customer Updated");
        }

        // PATCH (FromRoute + FromQuery)
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCustomerEmail(
            [FromRoute] int id,
            [FromQuery] string email)
        {
            await _service.PatchCustomerEmailAsync(id, email);
            return Ok("Email Updated");
        }

        // DELETE (FromRoute)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            await _service.DeleteCustomerAsync(id);
            return Ok("Customer Deleted");
        }

        // HEADER Example
        [HttpGet("header")]
        public IActionResult ReadHeader([FromHeader] string userAgent)
        {
            return Ok($"Header Value: {userAgent}");
        }
    }
}