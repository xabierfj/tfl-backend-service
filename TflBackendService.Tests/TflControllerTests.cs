using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using TflBackendService.Tests.TestUtils;

namespace TflBackendService.Tests;

[TestClass]
public class TflControllerTests
{
    private static HttpClient _client = null!;

    [ClassInitialize]
    public static void Setup(TestContext context)
    {
        var factory = new CustomWebApplicationFactory();
        _client = factory.CreateClient();
    }

    [TestMethod]
    public async Task GetLines_ReturnsFakeLines()
    {
        var response = await _client.GetAsync("/tfl/lines");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var lines = JsonSerializer.Deserialize<List<dynamic>>(json);

        Assert.IsNotNull(lines);
        Assert.AreEqual(2, lines!.Count);
    }
}
