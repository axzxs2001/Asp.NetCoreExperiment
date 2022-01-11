var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.Use(async (context, next) =>
{
    Console.WriteLine("{0}，第1个中间——前", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
    await next.Invoke();
    Console.WriteLine("{0}，第1个中间——后", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
});
app.Use(async (context, next) =>
{
    Console.WriteLine("{0}，第2个中间——前", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
    await next.Invoke();
    Console.WriteLine("{0}，第2个中间——后", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
});

app.UseThird();

app.MapGet("/test", () => "ok");

app.Run();


public static class ThirdMiddlewareExtensions
{
    public static void UseThird(this WebApplication app)
    {
        app.UseMiddleware<ThirdMiddleware>();
    }
}
public class ThirdMiddleware
{
    private readonly RequestDelegate _next;

    public ThirdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("{0}，第3个中间——前", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
        await _next(context);
        Console.WriteLine("{0}，第3个中间——后", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF"));
    }
}