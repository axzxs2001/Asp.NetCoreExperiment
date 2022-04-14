
var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();




app.MapGet("/test0/{name}", (string name) =>
{
    return "OK";
}).AddFilter((RouteHandlerInvocationContext context, RouteHandlerFilterDelegate next) =>
{
    var name = (string?)context.Parameters[0];
    if (name == "Bob")
    {
        return new ValueTask<object?>("No Bob's allowed");
    }
    return next(context);
});



app.MapGet("/test1/{name}", (string name) =>
{
    return "OK";
})
.AddFilter((RouteHandlerContext routeHandlerContext, RouteHandlerFilterDelegate next) =>
{
    return (context) =>
    {
        var name = (string?)context.Parameters[0];
        if (name == "Bob")
        {
            return new ValueTask<object?>("No Bob's allowed");
        }
        return next(context);
    };
});



app.MapGet("/test2/{name}", (string name) =>
{
    return "OK";
}).AddFilter<MyFilter>();


app.Run();


public class MyFilter : IRouteHandlerFilter
{
    public ValueTask<object?> InvokeAsync(RouteHandlerInvocationContext context, RouteHandlerFilterDelegate next)
    {
        var name = (string?)context.Parameters[0];
        if (name == "Bob")
        {
            return new ValueTask<object?>("No Bob's allowed");
        }
        return next(context);
    }
}