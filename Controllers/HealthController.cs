using Microsoft.AspNetCore.Mvc;

namespace TflBackendService.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "UP",
            timestamp = DateTime.UtcNow
        });
    }
}
