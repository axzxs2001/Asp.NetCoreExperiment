using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;
using OpenTracing;
using OpenTracing.Util;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenTracing();

builder.Services.AddSingleton<ITracer>(serviceProvider =>
{
    var serviceName = serviceProvider.GetRequiredService<IWebHostEnvironment>().ApplicationName;
    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
    Jaeger.Configuration.SenderConfiguration.DefaultSenderResolver = new SenderResolver(loggerFactory)
        .RegisterSenderFactory<ThriftSenderFactory>();

    var tracer = new Tracer.Builder(serviceName)
        .WithLoggerFactory(loggerFactory)
        .WithSampler(new ConstSampler(true))
        .Build();

    GlobalTracer.Register(tracer);
    return tracer;
});
var app = builder.Build();

app.MapGet("/stock/{no}", (string no, ILogger<Program> logger, ITracer tracer) =>
{
    using (var scope = tracer.BuildSpan("库存系统").StartActive(true))
    {
        logger.LogInformation("按{0}查询库存", no);
        return new Product { No = no, Quantity = 1324, Name = "Surface Go 3" };
    }
});

app.Run();

public class Product
{
    public string? No { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
}