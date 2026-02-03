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
    public async Task<IActionResult> GetLines(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string sortBy = "name",
        [FromQuery] string sortDirection = "asc")
    {
        if (page < 1)
        {
            return BadRequest("Page must be greater than 0.");
        }

        if (pageSize is < 1 or > 100)
        {
            return BadRequest("Page size must be between 1 and 100.");
        }

        var lines = await _tflApiClient.GetTubeLinesAsync();
        var normalizedSortBy = sortBy.Trim().ToLowerInvariant();
        var normalizedSortDirection = sortDirection.Trim().ToLowerInvariant();

        var sortedLines = normalizedSortBy switch
        {
            "id" => lines.OrderBy(line => line.Id, StringComparer.OrdinalIgnoreCase),
            "name" => lines.OrderBy(line => line.Name, StringComparer.OrdinalIgnoreCase),
            "modename" => lines.OrderBy(line => line.ModeName, StringComparer.OrdinalIgnoreCase),
            _ => null
        };

        if (sortedLines is null)
        {
            return BadRequest("SortBy must be one of: id, name, modeName.");
        }

        if (normalizedSortDirection is not ("asc" or "desc"))
        {
            return BadRequest("SortDirection must be 'asc' or 'desc'.");
        }

        var orderedLines = normalizedSortDirection == "desc"
            ? sortedLines.Reverse()
            : sortedLines;

        var totalCount = orderedLines.Count();
        var totalPages = totalCount == 0
            ? 0
            : (int)Math.Ceiling(totalCount / (double)pageSize);

        var pageItems = orderedLines
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var response = new Models.PagedResponse<Models.TflLine>
        {
            Items = pageItems,
            Pagination = new Models.PaginationMetadata
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            }
        };

        return Ok(response);
    }
}
