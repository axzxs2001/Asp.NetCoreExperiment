

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
        _logger.LogInformation("StartingAsync¡­¡­¡­¡­");    
        return Task.CompletedTask;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StartAsync¡­¡­¡­¡­");
        return Task.CompletedTask;
    }
    public Task StartedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StartedAsync¡­¡­¡­¡­");
        return Task.CompletedTask;
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StopAsync¡­¡­¡­¡­");
        return Task.CompletedTask;
    }
    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StoppedAsync¡­¡­¡­¡­");
        return Task.CompletedTask;
    }
    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StoppingAsync¡­¡­¡­¡­");
        return Task.CompletedTask;
    }
}