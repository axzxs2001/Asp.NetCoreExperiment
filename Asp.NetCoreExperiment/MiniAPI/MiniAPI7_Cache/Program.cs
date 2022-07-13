using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutputCache();

var app = builder.Build();

app.UseOutputCache();


var coolEndpointName = "coolPolicy";

//���������������������һ����������һ�����˾ͻ����Ϸ���ʧ�ܣ�0�����ÿ����м����ڵȴ�,NewestFirst����ѵȴ��Ķ���
//var options = new RateLimiterOptions().AddConcurrencyLimiter(coolEndpointName, new ConcurrencyLimiterOptions(1, QueueProcessingOrder.NewestFirst,1));

//�������ʱ�䲹��
//var options = new RateLimiterOptions().AddTokenBucketLimiter(coolEndpointName, new TokenBucketRateLimiterOptions(1, QueueProcessingOrder.OldestFirst, 1, TimeSpan.FromSeconds(10), 1));

//var options = new RateLimiterOptions().AddFixedWindowLimiter(coolEndpointName, new FixedWindowRateLimiterOptions(1, QueueProcessingOrder.OldestFirst, 1, TimeSpan.FromSeconds(10)));


var options = new RateLimiterOptions().AddSlidingWindowLimiter(coolEndpointName, new SlidingWindowRateLimiterOptions(1, QueueProcessingOrder.OldestFirst, 1, TimeSpan.FromSeconds(10),1));

//options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
//    {
//        return RateLimitPartition.CreateConcurrencyLimiter<string>("GlobalLimiter", key => new ConcurrencyLimiterOptions(1, QueueProcessingOrder.OldestFirst, 1));
//    });


app.UseRateLimiter(options);


app.MapGet("/nocached", () => DateTime.Now.ToString()).RequireRateLimiting(coolEndpointName);
app.MapGet("/nocached2", () => {  return DateTime.Now.ToString(); }).RequireRateLimiting(coolEndpointName); ;
app.MapGet("/cached", () => DateTime.Now.ToString()).CacheOutput();
app.MapGet("/query", () => DateTime.Now.ToString()).CacheOutput(p => p.VaryByQuery("key"));

app.Run();

