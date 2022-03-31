using Jaeger;
using OpenTracing.Propagation;
using OpenTracing;
using OpenTracing.Util;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Jaeger.Senders;
using Jaeger.Reporters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

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



app.MapGet("/order", async (ILogger<Program> logger, IHttpClientFactory clientFactory, ITracer tracer) =>
{
    using (var scope = tracer.BuildSpan("����ϵͳ").StartActive(true))
    {
        logger.LogInformation("������ѯ���");
        var client = clientFactory.CreateClient();
        var content = await client.GetStringAsync("http://localhost:5160/stock/D0001");
        return $"stock���ý����{content}";
    }
});

app.Run();

