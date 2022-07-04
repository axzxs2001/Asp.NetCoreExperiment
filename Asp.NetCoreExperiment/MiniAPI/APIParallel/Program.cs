using System.Threading.Channels;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/test", async () =>
{
    Console.WriteLine($"开始时间 {DateTime.Now.ToString("HH:mm:ss")}");
    var channels = new List<Channel<Parameter>>();
    var len = 5;
    //创建Channel,并关联ReadAsync方法
    for (int i = 0; i < len; i++)
    {
        var channel = Channel.CreateUnbounded<Parameter>(new UnboundedChannelOptions() { AllowSynchronousContinuations = true });
        channels.Add(channel);
        await Task.Factory.StartNew(async () =>
        {
            Console.WriteLine($"Task {i}");
            await ReadAsync(channel);
        });
    }
    //向Channel发送信息
    for (int i = 0; i < channels.Count; i++)
    {
        var channel = channels[i];
        await channel.Writer.WriteAsync(new Parameter { I = i });
        Console.WriteLine($"Write {i}");
    }

    //读取返回Channel，如果有返回True，API提前返回，留下的Channel给RemainingReadAsync执行
    for (int i = 0; i < channels.Count; i++)
    {
        var channel = channels[i];
        if (channel != null && await channel.Reader.WaitToReadAsync())
        {
            if (channel.Reader.TryRead(out var par))
            {
                if(par.Result)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine($"I:{par.I},Result:{par.Result},{DateTime.Now.ToString("HH:mm:ss")}");
                Console.ResetColor();
                if (par.Result)
                {
                    //把剩会的推到一个线程中执行
                    for (var r = i + 1; r < channels.Count; r++)
                    {
                        await Task.Factory.StartNew(async () =>
                        {
                            await RemainingReadAsync(channels[r]);
                        });
                    }
                    return TypedResults.Ok($"完成，有,{DateTime.Now.ToString("HH:mm:ss")}");

                }
            }
        }
    }
    return TypedResults.Ok($"完成，没有,{DateTime.Now.ToString("HH:mm:ss")}");
});

app.Run();
//处理剩余Channelr方法
async Task RemainingReadAsync(Channel<Parameter> channel)
{
    if (channel != null && await channel.Reader.WaitToReadAsync())
    {
        if (channel.Reader.TryRead(out var par))
        {
            Console.WriteLine($"I:{par.I},Result:{par.Result},{DateTime.Now.ToString("HH:mm:ss")}");
        }
    }
}
//读取Channel的方法
async Task ReadAsync(Channel<Parameter> channel)
{
    if (channel != null && await channel.Reader.WaitToReadAsync())
    {
        if (channel.Reader.TryRead(out var par))
        {
            await Task.Delay((par.I + 1) * 1000);
            if (par.I == 2)
            {
                par.Result = true;
            }
            else
            {
                par.Result = false;
            }
            await channel.Writer.WriteAsync(par);
        }
    }
}

//参数
class Parameter
{
    public int I { get; set; }
    public bool Result { get; set; }
}