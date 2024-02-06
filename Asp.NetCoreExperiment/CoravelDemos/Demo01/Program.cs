using Coravel;
using Coravel.Invocable;
using Coravel.Scheduling.Schedule;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScheduler();
//这里只注入无参的Invocable类
builder.Services.AddTransient<MyInvocable2>();

var app = builder.Build();

app.Services.UseScheduler(scheduler =>
 {
     scheduler
     .Schedule<MyInvocable2>()
     .EverySeconds(2);

     scheduler
     .ScheduleWithParams<MyInvocable>(3)
     .EverySeconds(3);
 });

app.Run();

public class MyInvocable : IInvocable
{
    readonly ILogger<MyInvocable> _logger;
    readonly int _seconds;
    public MyInvocable(ILogger<MyInvocable> logger, int seconds)
    {
        _logger = logger;
        _seconds = seconds;
    }
    public Task Invoke()
    {
        _logger.LogInformation("***每隔{int}秒执行一次", _seconds);
        return Task.CompletedTask;
    }
}

public class MyInvocable2 : IInvocable
{
    readonly ILogger<MyInvocable> _logger;
    public MyInvocable2(ILogger<MyInvocable> logger)
    {
        _logger = logger;
    }
    public Task Invoke()
    {
        _logger.LogInformation("---每隔两秒执行一次");
        return Task.CompletedTask;
    }
}