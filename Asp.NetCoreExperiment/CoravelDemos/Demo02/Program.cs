using Coravel;
using Coravel.Invocable;
using Coravel.Queuing.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<MyQueueInvocable>();
builder.Services.AddQueue();


var app = builder.Build();
//app.Services.ConfigureQueue();


// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (IQueue queue) =>
{
    //queue.QueueInvocable<MyQueueInvocable>();

    var guid = queue.QueueInvocableWithPayload<MyQueueInvocable, string>("开始Queue，添加时间：" + DateTime.Now);

    app.Logger.LogInformation(guid.ToString());
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


public class MyQueueInvocable : IInvocable, IInvocableWithPayload<string>
{

    readonly ILogger<MyQueueInvocable> _logger;
    public MyQueueInvocable(ILogger<MyQueueInvocable> logger)
    {
        _logger = logger;
    }
    public string Payload { get; set; }

    public async Task Invoke()
    {
        _logger.LogInformation(Payload + "，执行时间：{time}", DateTime.Now);
    }
}