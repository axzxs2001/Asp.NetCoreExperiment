using Coravel;
using Coravel.Invocable;
using Coravel.Scheduling.Schedule;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScheduler();

var app = builder.Build();

//app.Services.UseScheduler(scheduler =>
// {
//     scheduler.Schedule(
//         () => Console.WriteLine("每隔两2执行一次")
//     )
//     .EverySeconds(2);
// });

app.Services.UseScheduler(scheduler =>
 {
     scheduler
     .ScheduleWithParams<MyInvocable>(2)
     .EverySeconds(2);
 });


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



public class MyInvocable : IInvocable
{
    readonly int _seconds;
    public MyInvocable(ILogger<MyInvocable> logger, int seconds)
    {
        _seconds = seconds;
    }
    public Task Invoke()
    {
        Console.WriteLine($"每隔两{_seconds}执行一次");
        return Task.CompletedTask;
    }
}