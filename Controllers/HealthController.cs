using Microsoft.AspNetCore.Mvc;

namespace TflBackendService.Controllers;

/// <summary>
/// Provides health check information for monitoring and orchestration systems.
/// </summary>
[ApiController]
[Route("health")]
[Tags("Health")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Returns the current health status of the service.
    /// </summary>
    /// <returns>An object containing the status and current UTC timestamp.</returns>
    /// <response code="200">Service is healthy.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "UP",
            timestamp = DateTime.UtcNow
        });
    }
}
