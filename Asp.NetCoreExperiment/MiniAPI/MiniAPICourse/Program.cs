using MiniAPICourse.Services;

var builder = WebApplication.CreateBuilder();
builder.Services.AddScoped<ILogDemoService, LogDemoService>();
var app = builder.Build();

app.MapGet("/test", (ILogger<Program> logger, ILogDemoService logDemo) =>
{
    logger.LogInformation("test");
    logDemo.Demo01();
    return "ok";
});

app.Run();

namespace MiniAPICourse.Services
{
    public interface ILogDemoService
    {
        void Demo01();
    }
    public class LogDemoService : ILogDemoService
    {
        private readonly ILogger<LogDemoService> _logger;
        public LogDemoService(ILogger<LogDemoService> logger)
        {
            _logger = logger;
        }

        public void Demo01()
        {
            _logger.LogTrace("Trace");
            _logger.LogDebug("Debug");
            _logger.LogInformation("Information");
            _logger.LogWarning("Warning");
            _logger.LogError("Error");
            _logger.LogCritical("Critical");
        }
    }
}