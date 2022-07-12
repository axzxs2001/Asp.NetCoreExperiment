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


app.MapGet("/parame/{dataSource}", (IParameService parameService, string dataSource, string fields) =>
{
    app.Logger.LogInformation($"{dataSource}");

    return TypedResults.Json(
          parameService.GetParames(dataSource, fields),
              new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = false });
});

app.Run();


public interface IParameService
{
    IList<dynamic> GetParames(string dataSource, string fields);
}
public class ParameService : IParameService
{
    private readonly ILogger<ParameService> _logger;
    public ParameService(ILogger<ParameService> logger)
    {
        _logger = logger;
    }
    public IList<dynamic> GetParames(string dataSource, string fields)
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

            },
            {
                "order",
                 new List<dynamic>
                {
                    new { ID=1,Name="AAAA",Price=11.1m,Quantity=10},
                    new { ID=2,Name="BBBB",Price=12.2m,Quantity=12},
                    new { ID=3,Name="CCCC",Price=13.3m,Quantity=13},
                }
            }
        };

        _logger.LogInformation($"select {fields} from {dataSource}");
        return dir[dataSource.ToLower()];


    }
}