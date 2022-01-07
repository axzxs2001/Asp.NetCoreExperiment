var builder = WebApplication.CreateBuilder();

builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddTransient<ITransientService, TransientService>();
builder.Services.AddSingleton<ISingletonService, SingletonService>();


var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Path.HasValue)
    {
        switch (context.Request.Path.Value)
        {
            case string s when s.Contains("transient"):
                var transientService = context.RequestServices.GetService<ITransientService>();
                Console.WriteLine($"--------------{transientService?.Call()}");
                break;
            case string s when s.Contains("scoped"):
                var scopedService = context.RequestServices.GetService<IScopedService>();
                Console.WriteLine($"--------------{scopedService?.Call()}");
                break;
            case string s when s.Contains("singleton"):
                var singletonService = context.RequestServices.GetService<ISingletonService>();
                Console.WriteLine($"--------------{singletonService?.Call()}");
                break;
        }
    }
    await next.Invoke();
});

app.MapGet("/transient", (ITransientService transientService) => transientService.Call());

app.MapGet("/scoped", (IScopedService scopedService) => scopedService.Call());

app.MapGet("/singleton", (ISingletonService singletonService) => singletonService.Call());

app.Run();


public interface ITransientService
{
    string Call();
}
public class TransientService : ITransientService
{
    public DateTime Time { get; init; } = DateTime.Now;
    public string Call()
    {
        return $"TransientService {Time.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} test……";
    }
}

public interface IScopedService
{
    string Call();
}
public class ScopedService : IScopedService
{
    public DateTime Time { get; init; } = DateTime.Now;
    public string Call()
    {
        return $"ScopedService {Time.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} test……";      
    }
}

public interface ISingletonService
{
    string Call();
}
public class SingletonService : ISingletonService
{
    public DateTime Time { get; init; } = DateTime.Now;
    public string Call()
    {
        return $"TSingletonService {Time.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} test……";       
    }
}
