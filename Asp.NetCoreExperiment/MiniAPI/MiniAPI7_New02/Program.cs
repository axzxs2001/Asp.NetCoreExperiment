var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();

//app.AddEndpointFilter(async (ctx, next) =>
//{
//    // Log out the parameters
//    var logger = ctx.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>()
//        .CreateLogger("DemoFilter");

//    foreach (var p in ctx.Parameters)
//    {
//        logger.LogDebug("Parameter name {ParameterName}", p.Name);
//    }

//    await next();

//    // Log the return type
//    logger.LogDebug("Instance of {ResultType} returned", ctx.Result?.GetType());
//});

app.MapGet("/test/{id}", (int id) =>
{
    Console.WriteLine(id);
}).AddFilter<UnsupportedContentTypeFilter>();

app.Run();

public class UnsupportedContentTypeFilter : IEndpointFilter
{
    public override ValueTask<object> RunAsync(IEndpointFilterContext context, Func<IEndpointFilterContext, ValueTask<object?>> next)
    {
        // Assume last argument is some Boolean flag
        if (context.Parameters.Last())
        {
            var result = await next(context);
            if (result.ContentType != "application/json")
            {
                return new UnsupportedMediaTypeResult();
            }
            return result;
        }
    }
}
public class EndpointFilterContext
{
    public HttpContext HttpContext { get; }
    public IList<object?> Parameters { get; } // Not read-only to premit modifying of parameters by filters
}

public interface IEndpointFilter
{
    abstract ValueTask<object?> RunAsync(IEndpointFilterContext context, Func<IEndpointFilterContext, ValueTask<object?>> next);


     ValueTask OnBeforeAsync();


    ValueTask OnAfterAsync();
}