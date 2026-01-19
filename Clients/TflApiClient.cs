using System.Net.Http.Json;
using TflBackendService.Models;

namespace TflBackendService.Clients;

public class TflApiClient
{
    private readonly HttpClient _httpClient;

    public TflApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<TflLine>> GetTubeLinesAsync()
    {
        var response = await _httpClient
            .GetFromJsonAsync<IEnumerable<TflLine>>("Line/Mode/tube");

        return response ?? Enumerable.Empty<TflLine>();
    }
}
