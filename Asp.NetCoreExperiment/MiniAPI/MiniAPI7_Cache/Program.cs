using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutputCache();

var app = builder.Build();

app.UseOutputCache();


var limiterName = "MyLimiterName";
//并发限流，现在是如果有一个并发，另一个来了就会马上返回失败，0是设置可能有几个在等待,NewestFirst，会把等待的顶掉
//var options = new RateLimiterOptions().AddConcurrencyLimiter(limiterName, new ConcurrencyLimiterOptions(1, QueueProcessingOrder.NewestFirst, 1));

//间隔多少时间补发
var options = new RateLimiterOptions().AddTokenBucketLimiter(limiterName, new TokenBucketRateLimiterOptions(1, QueueProcessingOrder.OldestFirst, 1, TimeSpan.FromSeconds(8), 1));

//var options = new RateLimiterOptions().AddFixedWindowLimiter(limiterName, new FixedWindowRateLimiterOptions(1, QueueProcessingOrder.OldestFirst, 1, TimeSpan.FromSeconds(10)));


//var options = new RateLimiterOptions().AddSlidingWindowLimiter(limiterName, new SlidingWindowRateLimiterOptions(1, QueueProcessingOrder.OldestFirst, 1, TimeSpan.FromSeconds(10),1));

//options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
//    {
//        return RateLimitPartition.CreateConcurrencyLimiter<string>("GlobalLimiter", key => new ConcurrencyLimiterOptions(1, QueueProcessingOrder.OldestFirst, 1));
//    });


app.UseRateLimiter(options);


app.MapGet("/limit", () =>
{
    app.Logger.LogInformation($"limit 开始 {DateTime.Now}");
    Thread.Sleep(5000);
    app.Logger.LogInformation($"limit 结束 {DateTime.Now}");
    return DateTime.Now.ToString();
}).RequireRateLimiting(limiterName);


app.MapGet("/cached", () => DateTime.Now.ToString()).CacheOutput();
app.MapGet("/query", () => DateTime.Now.ToString()).CacheOutput(p => p.VaryByQuery("key").Expire(TimeSpan.FromMinutes(10)));

app.Run();

