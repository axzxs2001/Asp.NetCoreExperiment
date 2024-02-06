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
        app.Logger.LogInformation("�򵥵�Queue");
    });
    app.Logger.LogInformation(guid.ToString());
});

app.MapGet("/addqueue2", (IQueue queue) =>
{
    var guid = queue.QueueInvocableWithPayload<MyQueueInvocable, string>("��ʼQueue�����ʱ�䣺" + DateTime.Now);
    app.Logger.LogInformation(guid.ToString());
});

app.Run();

/// <summary>
/// ����
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
        _logger.LogInformation(Payload + "��ִ��ʱ�䣺{time}", DateTime.Now);
        await Task.CompletedTask;
    }
}