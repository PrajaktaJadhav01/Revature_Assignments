using Consul;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace ConsulDemo.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IConsulClient _consul;

        public OrderController(IConsulClient consul)
        {
            _consul = consul;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrder(int customerId)
        {
            var services = await _consul.Health.Service("customer-service", "", true);

            var service = services.Response.First().Service;

            var address = $"http://{service.Address}:{service.Port}/api/customer/{customerId}";

            var http = new HttpClient();

            var customer = await http.GetFromJsonAsync<object>(address);

            return Ok(new
            {
                OrderId = 1001,
                Product = "Laptop",
                Customer = customer
            });
        }
    }
}