using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Nodes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi("myapi", options =>
{    
    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_1;
    options.AddSchemaTransformer((schema, context, cancellationToken) =>
    {
        if (context.JsonTypeInfo.Type == typeof(WeatherForecast))
        {
            schema.Example = new JsonObject
            {
                ["date"] = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"),
                ["temperatureC"] = 0,
                ["temperatureF"] = 32,
                ["summary"] = "Bracing",
            };
        }
        return Task.CompletedTask;
    });
});
var app = builder.Build();
app.MapOpenApi("/openapi/{documentName}.json");
//app.MapGet("/scalar/{documentName}", (string documentName) => Results.Content($$"""
//              <!doctype html>
//              <html>
//              <head>
//                  <title>Scalar API Reference -- {{documentName}}</title>
//                  <meta charset="utf-8" />
//                  <meta
//                  name="viewport"
//                  content="width=device-width, initial-scale=1" />
//              </head>
//              <body>
//                  <script
//                  id="api-reference"
//                  data-url="/openapi/{{documentName}}.json"></script>
//                  <script>
//                  var configuration = {
//                      theme: 'purple',
//                  }              
//                  document.getElementById('api-reference').dataset.configuration =
//                      JSON.stringify(configuration)
//                  </script>
//                  <script src="https://cdn.jsdelivr.net/npm/@scalar/api-reference"></script>
//              </body>
//              </html>
//              """, "text/html")).ExcludeFromDescription();
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
