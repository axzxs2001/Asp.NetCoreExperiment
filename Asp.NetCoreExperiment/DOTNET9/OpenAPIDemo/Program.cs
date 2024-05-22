using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi("myapi", opt =>
{
    opt.UseTransformer((oper, context, c) =>
    {
        oper.Info = new OpenApiInfo
        {
            Version = "v1",
            Title = "My API",
            Description = "My API Description",
            Contact = new OpenApiContact
            {
                Name = "My Name",
                Email = "aaa@aa.com"
            },
            License = new OpenApiLicense
            {
                Name = "MIT",
                Url = new Uri("https://opensource.org/licenses/MIT")
            },
            TermsOfService = new Uri("https://www.google.com"),
        };
        return Task.CompletedTask;
    });
    opt.ShouldInclude = (o) =>
    {
        Console.WriteLine("-----HttpMethod------" + o.HttpMethod);
        Console.WriteLine("-----GroupName------" + o.GroupName);
        Console.WriteLine("------RelativePath-----" + o.RelativePath);
        Console.WriteLine("------ActionDescriptor-----" + o.ActionDescriptor.DisplayName);
        return true;
    };
    opt.UseOperationTransformer((oper, context, c) =>
    {
        Console.WriteLine("======summary=======" + oper.Summary);
        oper.Summary = "gui su wei test summary";
        oper.Tags = new OpenApiTag[]
        {
            new OpenApiTag
            {
                Name = "tag1", Description = "tag1 description",
            }
        };
        return Task.CompletedTask;
    });

});
var app = builder.Build();
app.MapOpenApi("/openapi/{documentName}.json");

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
app.MapGet("/test", () =>
{
    return new OkResult();
});
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
