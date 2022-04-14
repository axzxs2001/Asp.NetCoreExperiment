
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRouteHandlerFilter, MyFilter>();
var app = builder.Build();


//string SayHello(string name) => $"Hello, {name}!.";
//app.MapGet("/hello/{name}", SayHello)
//    .AddFilter(async (RouteHandlerInvocationContext context, RouteHandlerFilterDelegate next) =>
//    {
//        return await next(context);
//    });


//app.MapGet("/hello/{name}", (string name) => $"Hello, {name}!.")
//    .AddFilter(async (RouteHandlerInvocationContext context, RouteHandlerFilterDelegate next) =>
//    {
//        return await next(context);
//    });


Data GetData(string no)
{
    Console.WriteLine($"Get方法中：no={no}");
    return new Data { No = no, Name = "test" + DateTime.Now };
};


app.MapGet("/data1/{no}", GetData)
    .AddFilter((RouteHandlerInvocationContext context, RouteHandlerFilterDelegate next) =>
    {
        var no = (string?)context.Parameters[0];
        Console.WriteLine($"Get方法前： no={no}");
        if (no != null && !no.StartsWith("NO"))
        {
            return new ValueTask<object?>("no is error!");
        }
        var result = next(context);
        if (result.IsCompleted)
        {
            Console.WriteLine($"Get方法后：结果={result.Result}");
        }
        return result;
    });


string AddTest(Data data)
{
    Console.WriteLine($"Post方法中：no={data.No}");
    return "OK";
}
app.MapPost("/data1", AddTest)
    .AddFilter((RouteHandlerInvocationContext context, RouteHandlerFilterDelegate next) =>
    {
        var data = (Data?)context.Parameters[0];
        Console.WriteLine($"Post方法前： data={data}");
        var result = next(context);
        if (result.IsCompleted)
        {
            Console.WriteLine($"Post方法后：结果={result.Result}");
        }
        return result;
    });




app.MapGet("/data2/{no}", GetData)
.AddFilter((RouteHandlerContext routeHandlerContext, RouteHandlerFilterDelegate next) =>
{
    return (context) =>
    {
        var no = (string?)context.Parameters[0];
        if (no != null && !no.StartsWith("NO"))
        {
            return new ValueTask<object?>("no is error!");
        }
        return next(context);
    };
});



app.MapGet("/data3/{name}", GetData).AddFilter<MyFilter>();


app.Run();


public class MyFilter : IRouteHandlerFilter
{
    public ValueTask<object?> InvokeAsync(RouteHandlerInvocationContext context, RouteHandlerFilterDelegate next)
    {
        var no = (string?)context.Parameters[0];
        if (no != null && !no.StartsWith("NO"))
        {
            return new ValueTask<object?>("no is error!");
        }
        return next(context);
    }
}

public record Data
{
    public string No { get; set; }
    public string Name { get; set; }
}