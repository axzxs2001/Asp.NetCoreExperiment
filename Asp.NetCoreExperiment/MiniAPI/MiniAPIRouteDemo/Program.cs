using System.Collections;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.MapEndpoint<MyEndpoint>();

app.MapOrderEndpoint();

app.Run();


static class EndpointExtensions
{
    public static WebApplication MapEndpoint<T>(this WebApplication app) where T : IEndpoint
    {
        app.MapMethods(
            T.Path,
            new[] { T.HttpMethod.ToString() },
            T.Hanler
            );

        return app;
    }
}
public interface IEndpoint
{
    static abstract string Path { get; }
    static abstract HttpMethod HttpMethod { get; }
    static abstract Delegate Hanler { get; }
}
class MyEndpoint : IEndpoint
{
    public static string Path => "/test/{id}";

    public static HttpMethod HttpMethod => HttpMethod.Get;

    public static Delegate Hanler => GetOrder;

    private static IResult GetOrder(string? id)
    {
        return TypedResults.Ok($"TEST:{id}");
    }
}









static class OrderEndpoint
{

    public static void MapOrderEndpoint(this WebApplication app)
    {
        app.MapGet("/order/{orderno}", (string orderno) =>
        {
            return TypedResults.Ok();
        });
        app.MapPut("/order", (string orderno) =>
        {
            return TypedResults.Ok();
        });
    }
}