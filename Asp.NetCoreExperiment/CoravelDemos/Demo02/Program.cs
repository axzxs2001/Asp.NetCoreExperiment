using Coravel;
using Coravel.Invocable;
using Coravel.Queuing.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<MyQueueInvocable>();
builder.Services.AddQueue();

var app = builder.Build();

app.MapGet("/addqueue1", (IQueue queue) =>
{
    var guid = queue.QueueTask(() =>
    {
        app.Logger.LogInformation("简单的Queue");
    });
    app.Logger.LogInformation(guid.ToString());
});

app.MapGet("/addqueue2", (IQueue queue) =>
{
    var guid = queue.QueueInvocableWithPayload<MyQueueInvocable, string>("开始Queue，添加时间：" + DateTime.Now);
    app.Logger.LogInformation(guid.ToString());
});

app.Run();

/// <summary>
/// 负载
/// </summary>
public class MyQueueInvocable : IInvocable, IInvocableWithPayload<string>
{
    readonly ILogger<MyQueueInvocable> _logger;
    public MyQueueInvocable(ILogger<MyQueueInvocable> logger)
    {
        _logger = logger;
    }
    public string Payload { get; set; } = string.Empty;
    public async Task Invoke()
    {
        _logger.LogInformation(Payload + "，执行时间：{time}", DateTime.Now);
        await Task.CompletedTask;
    }
}