//using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MiniDemo05;

var builder = WebApplication.CreateBuilder(args);

//A,B
//builder.Services.AddHealthChecks();

//C

builder.Services.AddHealthChecks()
   .AddCheck<TestHealthCheck>("test_health_check");

//D
//builder.Services.AddHealthChecks()
//    .AddCheck("Foo", () =>
//        HealthCheckResult.Healthy("Foo is OK!"), tags: new[] { "foo_tag" })
//    .AddCheck("Bar", () =>
//        HealthCheckResult.Unhealthy("Bar is unhealthy!"), tags: new[] { "bar_tag" })
//    .AddCheck("Baz", () =>
//        HealthCheckResult.Healthy("Baz is OK!"), tags: new[] { "baz_tag" });
//E
//builder.Services
//    .AddHealthChecks()
//    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),"select 1");

//F
//builder.Services.AddHostedService<StartupHostedService>();
//builder.Services.AddSingleton<StartupHostedServiceHealthCheck>();

//builder.Services.AddHealthChecks()
//    .AddCheck<StartupHostedServiceHealthCheck>(
//        "hosted_service_startup",
//        failureStatus: HealthStatus.Degraded,
//        tags: new[] { "ready" });

//publisher
builder.Services.Configure<HealthCheckPublisherOptions>(options =>
{
    options.Delay = TimeSpan.FromSeconds(1);
    options.Period = TimeSpan.FromSeconds(5);
  
    //options.Predicate = (check) => check.Tags.Contains("ready");
});

builder.Services.AddSingleton<IHealthCheckPublisher, HealthPublisher>();


var app = builder.Build();

app.MapGet("/test", () =>
{

    return DateTime.Now;
});
//A,C,E
app.MapHealthChecks("/health");
//B
//app.MapHealthChecks("/health", new HealthCheckOptions
//{
//    //AllowCachingResponses = false,
//    ResponseWriter = (context, result) =>
//   {
//       var backContent = "this is health";
//       return context.Response.WriteAsync(backContent);
//   }
//});
//D
//app.MapHealthChecks("/health", new HealthCheckOptions()
// {
//     Predicate = (check) => check.Tags.Contains("foo_tag") ||
//         check.Tags.Contains("bar_tag")
// });

//F
//app.MapHealthChecks("/health/ready", new HealthCheckOptions()
//{
//    Predicate = (check) => check.Tags.Contains("ready"),
//});

//app.MapHealthChecks("/health/live", new HealthCheckOptions()
//{
//    Predicate = (_) => false
//});

app.Run();


public class TestHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var client = new MemoryMetricsClient();
        var metrics = client.GetMetrics();
        Console.WriteLine("Total: " + metrics.Total);
        Console.WriteLine("Used : " + metrics.Used);
        Console.WriteLine("Free : " + metrics.Free);

        var healthCheckResultHealthy = metrics.Free > 6000;
        if (healthCheckResultHealthy)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("A healthy result."));
        }

        return Task.FromResult(
            new HealthCheckResult(context.Registration.FailureStatus,
            "An unhealthy result."));

    }
}


public class StartupHostedServiceHealthCheck : IHealthCheck
{
    private volatile bool _startupTaskCompleted = false;

    public string Name => "slow_dependency_check";

    public bool StartupTaskCompleted
    {
        get => _startupTaskCompleted;
        set => _startupTaskCompleted = value;
    }

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        if (StartupTaskCompleted)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("The startup task is finished."));
        }

        return Task.FromResult(
            HealthCheckResult.Unhealthy("The startup task is still running."));
    }
}

public class StartupHostedService : IHostedService, IDisposable
{
    private readonly int _delaySeconds = 15;
    private readonly ILogger _logger;
    private readonly StartupHostedServiceHealthCheck _startupHostedServiceHealthCheck;

    public StartupHostedService(ILogger<StartupHostedService> logger,
        StartupHostedServiceHealthCheck startupHostedServiceHealthCheck)
    {
        _logger = logger;
        _startupHostedServiceHealthCheck = startupHostedServiceHealthCheck;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Startup Background Service is starting.");

        // Simulate the effect of a long-running startup task.
        Task.Run(async () =>
        {
            await Task.Delay(_delaySeconds * 1000);

            _startupHostedServiceHealthCheck.StartupTaskCompleted = true;

            _logger.LogInformation("Startup Background Service has started.");
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Startup Background Service is stopping.");

        return Task.CompletedTask;
    }

    public void Dispose()
    {
    }
}


public class HealthPublisher : IHealthCheckPublisher
{
    private readonly ILogger<HealthPublisher> _logger;
    public HealthPublisher(ILogger<HealthPublisher> logger)
    {
        _logger = logger;
    }

    public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{DateTime.Now.ToString()},{ report.Status}");
        return Task.CompletedTask;

    }
}