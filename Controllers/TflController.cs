using Microsoft.AspNetCore.Mvc;
using TflBackendService.Clients;
using TflBackendService.Dtos;

namespace TflBackendService.Controllers;

/// <summary>
/// Exposes normalized Transport for London data.
/// </summary>
[ApiController]
[Route("tfl")]
[Tags("TfL")]
[Produces("application/json")]
public class TflController : ControllerBase
{
    private readonly TflApiClient _tflApiClient;

    /// <summary>
    /// Initializes a new instance of <see cref="TflController"/> with the specified TfL API client.
    /// </summary>
    /// <param name="tflApiClient">The client used to communicate with the TfL API.</param>
    public TflController(TflApiClient tflApiClient)
    {
        _tflApiClient = tflApiClient;
    }

    /// <summary>
    /// Retrieves all TfL tube lines.
    /// </summary>
    /// <returns>A list of tube lines with their id, name, and mode.</returns>
    /// <response code="200">Returns the list of tube lines.</response>
    [HttpGet("lines")]
    [ProducesResponseType(typeof(IEnumerable<TflLineResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLines()
    {
        var lines = await _tflApiClient.GetTubeLinesAsync();
        var response = lines.Select(l => new TflLineResponse
        {
            Id = l.Id,
            Name = l.Name,
            ModeName = l.ModeName
        });
        return Ok(response);
    }
}
