using Microsoft.AspNetCore.Mvc;
using TflBackendService.Clients;

namespace TflBackendService.Controllers;

[ApiController]
[Route("tfl")]
public class TflController : ControllerBase
{
    private readonly TflApiClient _tflApiClient;

    public TflController(TflApiClient tflApiClient)
    {
        _tflApiClient = tflApiClient;
    }

    [HttpGet("lines")]
    public async Task<IActionResult> GetLines()
    {
        var lines = await _tflApiClient.GetTubeLinesAsync();
        return Ok(lines);
    }
}
