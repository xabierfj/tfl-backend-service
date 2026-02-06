using System.Reflection;
using TflBackendService.Clients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TfL Backend Service",
        Version = "v1",
        Description = "Backend service that consumes and normalizes data from the Transport for London (TfL) Open Data API."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddControllers();

builder.Services.AddHttpClient<TflApiClient>(client =>
{
    client.BaseAddress = new Uri("https://api.tfl.gov.uk/");
    client.Timeout = TimeSpan.FromSeconds(10);
});

var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}





app.Run();
public partial class Program { }