using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);
// 注入内存缓存服务
builder.Services.AddMemoryCache();
//加载ClientRateLimiting配置文件
//builder.Services.Configure<ClientRateLimitOptions>(builder.Configuration.GetSection("ClientRateLimiting"));
//加载IPRateLimiting配置文件
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

//加载ClientRateLimitPolicies配置文件
//builder.Services.Configure<ClientRateLimitPolicies>(builder.Configuration.GetSection("ClientRateLimitPolicies"));
//加载IPRateLimitPolicies配置文件
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));

// 注入限流内存缓存服务
builder.Services.AddInMemoryRateLimiting();

// inject IP counter and rules cache stores
//builder.Services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
//builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();


// 注入限流配置文件服务
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();


var app = builder.Build();

//启用ClientRateLimitPolicies
//var clientPolicyStore = app.Services.GetRequiredService<IClientPolicyStore>();
//await clientPolicyStore.SeedAsync();

//启用IPRateLimitPolicies
var ipPolicyStore = app.Services.GetRequiredService<IIpPolicyStore>();
await ipPolicyStore.SeedAsync();

//使用Client限流中间件
//app.UseClientRateLimiting();


//使用Ip限流中间件
app.UseIpRateLimiting();

app.MapGet("/test00", () =>
{
    return "get test00 ok";
});

app.MapGet("/test01", () =>
{
    return "get test01 ok";
});

app.MapGet("/test02", () =>
{
    return "get test02 ok";
});
app.MapPost("/test02", () =>
{
    return "post test02 ok";
});
app.MapGet("/test03", () =>
{
    return "get test01 ok";
});
app.Run();

