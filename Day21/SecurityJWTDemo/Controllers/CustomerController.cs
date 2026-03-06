using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Policy = "MinimumAgePolicy")]
public class CustomerController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCustomer()
    {
        return Ok(new { Name = "John Doe", Age = 30 });
    }
}