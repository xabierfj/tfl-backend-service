using System.Net.Http.Json;
using TflBackendService.Models;

namespace TflBackendService.Clients;

public class TflApiClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of <see cref="TflApiClient"/> with the specified HTTP client.
    /// </summary>
    /// <param name="httpClient">The HTTP client configured for the TfL API.</param>
    public TflApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Fetches all tube lines from the TfL API.
    /// </summary>
    /// <returns>A collection of <see cref="TflLine"/> objects, or an empty collection if none are found.</returns>
    public async Task<IEnumerable<TflLine>> GetTubeLinesAsync()
    {
        var response = await _httpClient
            .GetFromJsonAsync<IEnumerable<TflLine>>("Line/Mode/tube");

        return response ?? Enumerable.Empty<TflLine>();
    }
}
