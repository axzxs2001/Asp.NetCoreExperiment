using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Internal;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("MyRedisConStr");
    options.InstanceName = "DistributedRedis_";
});

//builder.Services.AddMemoryCache(opt =>
//{

//    // opt.SizeLimit = 40;

//});

var app = builder.Build();


//app.MapGet("/get/{id}", (IMemoryCache memoryCache, string id) =>
//{
//    var result = memoryCache.TryGetValue(id, out string timeStr);
//    if (result)
//    {
//        return $"获取成功：{timeStr}";
//    }
//    return "获取失败";
//});
//app.MapGet("/set/{id}", (IMemoryCache memoryCache, string id) =>
//{
// var time = memoryCache.Set<string>(id, $"{id}、 {DateTime.Now}", TimeSpan.FromSeconds(50));
//var time = memoryCache.Set<string>(id, $"{id}、 {DateTime.Now}", new MemoryCacheEntryOptions
//{
//    Size = 10,        

//});

//var time = memoryCache.Set<string>(id, $"{id}、 {DateTime.Now}", new MemoryCacheEntryOptions
//{
//    //3秒内不访问过期
//    SlidingExpiration = TimeSpan.FromSeconds(3),
//    //间隔少于3秒内一直有访问，则30秒过期
//    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),

//});




//  return $"设置的时间为：{time}";
//});
app.MapGet("/disget/{id}", async (IDistributedCache distributedCache, string id) =>
 {

     var result = await distributedCache.GetStringAsync(id);
     return $"获取成功：{result}";

 });
app.MapGet("/disset/{id}", async (IDistributedCache distributedCache, string id) =>
{
    var time = $"{id}、 {DateTime.Now}";
    await distributedCache.SetStringAsync(id, time, new DistributedCacheEntryOptions
    {
        SlidingExpiration = TimeSpan.FromSeconds(5),
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)

    });

    return $"设置的时间为：{time}";
});
app.Run();
