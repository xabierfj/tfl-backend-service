using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TflBackendService.Clients;
using TflBackendService.Tests.TestUtils;
using Microsoft.AspNetCore.Hosting;

namespace TflBackendService.Tests.TestUtils;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(TflApiClient));

            var fakeJson = """
            [
              { "id": "central", "name": "Central", "modeName": "tube" },
              { "id": "victoria", "name": "Victoria", "modeName": "tube" }
            ]
            """;

            var fakeHandler = new FakeHttpMessageHandler(fakeJson);
            var httpClient = new HttpClient(fakeHandler)
            {
                BaseAddress = new Uri("https://api.tfl.gov.uk/")
            };

            services.AddSingleton(new TflApiClient(httpClient));
        });
    }
}
