using Coravel;
using Coravel.Cache.Interfaces;
using Coravel.Cache.PostgreSQL;
using Coravel.Cache.SQLServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCache();
builder.Services.AddSQLServerCache(connectionString);
builder.Services.AddPostgreSQLCache(connectionString);
builder.Services.AddCache(new RedisCache());



// Or, if you need the service provider to create your object:
builder.Services.AddCache(provider => new RedisCache(provider.GetService<ISomeRegisteredInterface>()));

var app = builder.Build();


app.MapGet("/add", async (ICache cache) =>
{
    await cache.RememberAsync("time", async () =>
    {
        return await Task.FromResult(DateTime.Now);
    }, TimeSpan.FromSeconds(10));

});
app.MapGet("/read", async (ICache cache) =>
{
    
    if (await cache.HasAsync("time"))
    {
        return await cache.GetAsync<DateTime>("time");
    }
    else
    {
        return DateTime.MinValue;
    }
});

app.Run();

class RedisCache : ICache
{
    public void Flush()
    {
        throw new NotImplementedException();
    }

    public TResult Forever<TResult>(string key, Func<TResult> cacheFunc)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> ForeverAsync<TResult>(string key, Func<Task<TResult>> cacheFunc)
    {
        throw new NotImplementedException();
    }

    public void Forget(string key)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> GetAsync<TResult>(string key)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasAsync(string key)
    {
        throw new NotImplementedException();
    }

    public TResult Remember<TResult>(string key, Func<TResult> cacheFunc, TimeSpan expiresIn)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> RememberAsync<TResult>(string key, Func<Task<TResult>> cacheFunc, TimeSpan expiresIn)
    {
        throw new NotImplementedException();
    }
}