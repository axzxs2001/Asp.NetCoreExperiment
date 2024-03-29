

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddHostedService<MyService>();

var app = builder.Build();

app.MapGet("/", () => true);


app.Run();


public class MyService : IHostedLifecycleService
{
    private readonly ILogger<MyService> _logger;
    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }
    public Task StartingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StartingAsyncˇ­ˇ­ˇ­ˇ­");    
        return Task.CompletedTask;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StartAsyncˇ­ˇ­ˇ­ˇ­");
        return Task.CompletedTask;
    }
    public Task StartedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StartedAsyncˇ­ˇ­ˇ­ˇ­");
        return Task.CompletedTask;
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StopAsyncˇ­ˇ­ˇ­ˇ­");
        return Task.CompletedTask;
    }
    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StoppedAsyncˇ­ˇ­ˇ­ˇ­");
        return Task.CompletedTask;
    }
    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StoppingAsyncˇ­ˇ­ˇ­ˇ­");
        return Task.CompletedTask;
    }
}