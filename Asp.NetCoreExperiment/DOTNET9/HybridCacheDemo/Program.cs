using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
    {
        EndPoints = { "localhost:6379" },
        AbortOnConnectFail = false
    };
});
builder.Services.AddHybridCache();

var app = builder.Build();

app.MapGet("/time0", async (IMemoryCache cache) =>
{
    return await cache.GetOrCreateAsync("time0", async _ => await Task.FromResult(DateTime.Now));

});

app.MapGet("/time1", async (IDistributedCache cache) =>
{
    var time = await cache.GetStringAsync("time1");
    if (string.IsNullOrWhiteSpace(time))
    {
        time = DateTime.Now.ToString();
        await cache.SetStringAsync("time1", time);
    }
    return time;

});

app.MapGet("/time2", async (HybridCache cache) =>
{
    return await cache.GetOrCreateAsync("time2", async _ => await Task.FromResult(DateTime.Now));

});
app.Run();

