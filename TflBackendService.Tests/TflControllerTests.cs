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
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        var items = root.GetProperty("items");
        var pagination = root.GetProperty("pagination");

        Assert.AreEqual(2, items.GetArrayLength());
        Assert.AreEqual(1, pagination.GetProperty("page").GetInt32());
        Assert.AreEqual(10, pagination.GetProperty("pageSize").GetInt32());
        Assert.AreEqual(2, pagination.GetProperty("totalCount").GetInt32());
        Assert.AreEqual(1, pagination.GetProperty("totalPages").GetInt32());
    }
}
