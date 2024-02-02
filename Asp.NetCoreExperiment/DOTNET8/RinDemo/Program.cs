var builder = WebApplication.CreateBuilder(args);


builder.Logging.AddRinLogger();
builder.Services.AddRin();

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseRin();
app.UseRinDiagnosticsHandler();

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

app.MapPost("/weatherforecast", (WeatherForecast weather) =>
{
    return weather;
});
app.MapPut("/weatherforecast", (WeatherForecast weather) =>
{
    return weather;
});
app.MapDelete("/weatherforecast", (int id) =>
{
    return true;
});


app.MapGet("/weather", async (IHttpClientFactory clientFactory) =>
{
    var lat = "35.71459525981295";
    var lon = "139.7989249730396";
    var apiKey = "6d43c019aa2aeb0e7dd5ce070f174248";
    var client = clientFactory.CreateClient();
    var url = $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid={apiKey}";
    Console.WriteLine(url);
    var response = await client.GetAsync(url);
    return response.Content.ReadAsStringAsync();
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
