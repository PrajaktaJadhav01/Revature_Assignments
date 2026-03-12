using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HealthDemo.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var watch = new Stopwatch();

            // Database simulation
            watch.Start();

            // Here normally we would query the database
            await Task.Delay(50); 

            watch.Stop();

            var databaseReadTime = watch.ElapsedMilliseconds;

            // Call Customer Service API
            var httpClient = new HttpClient();

            watch.Restart();

            var response = await httpClient.GetAsync("http://localhost:5200/api/customer");

            watch.Stop();

            var customerServiceStatus = response.IsSuccessStatusCode;
            var customerReadTime = watch.ElapsedMilliseconds;

            var result = new HealthDTO
            {
                DatabaseStatus = true,
                DatabaseReadTimeInMilliseconds = databaseReadTime,
                CustomerServiceStatus = customerServiceStatus,
                CustomerReadTimeInMilliseconds = customerReadTime
            };

            return Ok(result);
        }
    }

    public class HealthDTO
    {
        public bool DatabaseStatus { get; set; }
        public long DatabaseReadTimeInMilliseconds { get; set; }

        public bool CustomerServiceStatus { get; set; }
        public long CustomerReadTimeInMilliseconds { get; set; }
    }
}