using System.Threading.Channels;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHostedService<Processor>();
builder.Services.AddSingleton<Channel<ChannelRequest>>(_ =>
{

    return Channel.CreateUnbounded<ChannelRequest>(new UnboundedChannelOptions
    {
        SingleReader = true,
        AllowSynchronousContinuations = false,
    });
});
var app = builder.Build();




app.MapGet("/send", async (Channel<ChannelRequest> channel) =>
{
    await channel.Writer.WriteAsync(new ChannelRequest($"Hello {DateTime.Now}"));
    return Results.Ok();
});

app.Run();

public record ChannelRequest(string Message);
public class Processor : BackgroundService
{
    private readonly Channel<ChannelRequest> _channel;

    public Processor(Channel<ChannelRequest> channel)
    {
        _channel = channel;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _channel.Reader.WaitToReadAsync(stoppingToken))
        {
            var request = await _channel.Reader.ReadAsync(stoppingToken);
            await Task.Delay(2000,stoppingToken);
            Console.WriteLine(request.Message);
        }
    }
}