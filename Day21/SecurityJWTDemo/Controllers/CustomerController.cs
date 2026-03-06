using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class CustomerController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCustomer()
    {
        return Ok(new { Name = "John Doe", Age = 30 });
    }
}