namespace API.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")] 
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Welcome to new  API");
    }
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok(new { status = "API is healthy", timestamp = DateTime.UtcNow });
    }
}
