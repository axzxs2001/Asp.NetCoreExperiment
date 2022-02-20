
using NLog;
using NLog.Web;

//启动日志
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);
    //配置日志
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    var app = builder.Build();
    //使用日志
    app.MapGet("/logtest", () =>
    {
        app.Logger.LogTrace("LogTrace");
        app.Logger.LogDebug("LogDebug");
        app.Logger.LogWarning("LogWarning");
        app.Logger.LogInformation("LogInformation");
        app.Logger.LogError("LogError");
        app.Logger.LogCritical(new Exception("eLogCritical"), "LogCritical");
        return "logtest";
    });

    app.MapGet("/myvalue", MyService.GetMyValue);

    app.Run();
}
catch (Exception exception)
{
    //异常时处理日志
    logger.Fatal(exception, "Stopped program because of exception");
}
finally
{
    NLog.LogManager.Shutdown();
}

class MyService
{
    public static string GetMyValue(ILogger<MyService> logger)
    {
        logger.LogInformation("TestService.GetMyValue");
        return "MyValue";
    }
}