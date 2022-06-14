internal static class EndpointsExtensionsBase
{
    public static WebApplication MapEndpoint<IEnumerable<T>>(this WebApplication app) where T : IEndpoint
    {

        return app;
    }
}