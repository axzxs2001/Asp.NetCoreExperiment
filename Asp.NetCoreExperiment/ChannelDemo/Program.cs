using System.Threading.Channels;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHostedService<VectorService>();
builder.Services.AddSingleton<Channel<VectorData>>(_ =>
{
    //return Channel.CreateUnbounded<VectorData>(new UnboundedChannelOptions
    //{
    //    SingleReader = true,
    //    AllowSynchronousContinuations = false,
    //});
    return Channel.CreateBounded<VectorData>(new BoundedChannelOptions(10)
    {
        SingleReader = true,
        FullMode = BoundedChannelFullMode.Wait,
        AllowSynchronousContinuations = false,
    });
});
var app = builder.Build();


app.MapGet("/vector", async (Channel<VectorData> channel) =>
{
    await channel.Writer.WriteAsync(new VectorData($"这里是用户内容类信息，{DateTime.Now.ToString("yyyyMMddHHmmssfff")}"));
    return Results.Ok();
});

app.Run();
public record VectorData(string content);
public class VectorService : BackgroundService
{
    private readonly Channel<VectorData> _channel;

    public VectorService(Channel<VectorData> channel)
    {
        _channel = channel;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _channel.Reader.WaitToReadAsync(stoppingToken))
        {
            var vectorData = await _channel.Reader.ReadAsync(stoppingToken);
            Console.WriteLine($"开始向量化处理：{vectorData.content}");
            await Task.Delay(3000, stoppingToken);
            Console.WriteLine($"向量化处理结束");
        }
    }
}