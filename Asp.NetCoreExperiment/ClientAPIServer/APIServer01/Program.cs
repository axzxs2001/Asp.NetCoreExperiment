var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCaching();
builder.Services.AddSingleton<IParameService, ParameService>();

var app = builder.Build();

app.UseResponseCaching();
app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl =
        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
        {
            Public = true,
            MaxAge = TimeSpan.FromHours(1)
        };
    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
        new string[] { "Accept-Encoding" };

    await next();
});


app.MapGet("/kv/{*path}", (IParameService parameService, string path) =>
{
    app.Logger.LogInformation($"{path}");
    return TypedResults.Json(
          parameService.GetParames(path),
          new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = false });
});

app.Run();


public interface IParameService
{
    IList<dynamic> GetParames(string path);
}
public class ParameService : IParameService
{
    private readonly ILogger<ParameService> _logger;
    public ParameService(ILogger<ParameService> logger)
    {
        _logger = logger;
    }
    public IList<dynamic> GetParames(string path)
    {
        var dir = new Dictionary<string, List<dynamic>>()
        {
            {
                "type",
                new List<dynamic>
                {
                    new { ID=1,Name="AliPay"},
                    new { ID=2,Name="MiroPay"},
                    new { ID=3,Name="PayPay"}
                }
            },
            {
                "city",
                new List<dynamic>
                {
                    new { Key=1,Value="BeiJing"},
                    new { Key=2,Value="ShangHai"},
                    new { Key=3,Value="ShenZhen"}
                }
            }
        };
        var parames = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
        if (parames.Length >= 3)
        {
            _logger.LogInformation($"select {parames[1]},{parames[2]} from {parames[0]}");
        }
        return dir[parames[0].ToLower()];
    }
}