var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton(new List<A> { new A { } });
var app = builder.Build();


app.UseMiddleware<MiddlewareTest>();
app.MapGet("/weatherforecast", (List<A>  list) =>
{
  
});

app.Run();
internal class MiddlewareTest
{
    RequestDelegate _next;
    public MiddlewareTest(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, List<A> list)
    {
       await context.Response.WriteAsync("MiddlewareTest");
    }
    
}

class A
{
    public A()
    {

    }

}