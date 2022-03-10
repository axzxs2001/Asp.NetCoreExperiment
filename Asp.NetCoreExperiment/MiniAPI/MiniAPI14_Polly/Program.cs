using Polly;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpClient("RetryClient", httpclient =>
    {
        httpclient.BaseAddress = new Uri("http://localhost:5258");
    })
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(3, retryNumber =>
        {
            switch (retryNumber)
            {
                case 1:
                    return TimeSpan.FromMilliseconds(500);
                case 2:
                    return TimeSpan.FromMilliseconds(1000);
                case 3:
                    return TimeSpan.FromMilliseconds(1500);
                default:
                    return TimeSpan.FromMilliseconds(100);
            }
        }))
    .AddTransientHttpErrorPolicy(policyBuilder =>
  policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

//在5秒内4次请求，如果50%失败，就熔断10秒
//.AddTransientHttpErrorPolicy(policyBuilder =>
//  policyBuilder.AdvancedCircuitBreakerAsync(0.5d, TimeSpan.FromSeconds(5), 4, TimeSpan.FromSeconds(10)));

//.AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)));

//.AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.RetryAsync(3));

//一直重试
//.AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.RetryForeverAsync());
//每2秒重试一次
//.AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryForeverAsync(retryNumber =>
//{
//    Console.WriteLine(retryNumber);
//    return TimeSpan.FromSeconds(2);
//}));


var app = builder.Build();


app.MapGet("/test", async (IHttpClientFactory httpClientFactory) =>
{
    try
    {
        var httpClient = httpClientFactory.CreateClient("RetryClient");
        var content = await httpClient.GetStringAsync("other-api");
        Console.WriteLine(content);
        return "ok";
    }
    catch (Exception exc)
    {
        if (!Count.Time.HasValue)
        {
            Count.Time = DateTime.Now;
        }
        return $"{exc.Message}    【次数：{Count.I++}】  【{Count.Time.Value.ToString("yyyy-MM-dd HH:mm:ss.fffffff")}】";
    }
});


app.MapGet("/other-api", (ILogger<Program> logger) =>
{
    //if (DateTime.Now.Second % 3 == 0)
    //{
    //    logger.LogInformation($"成功:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")}");
    //    return Results.StatusCode(200);
    //}
    //else
    //{
    logger.LogInformation($"失败:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")}");
    return Results.StatusCode(500);
    //}
});
app.Run();

static class Count
{
    public static int I = 1;

    public static DateTime? Time;
}

