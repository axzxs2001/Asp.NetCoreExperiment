using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Web;

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


app.MapGet("/parame/{dataSource}", (IParameService parameService, string dataSource, string fields, string conditions) =>
{
  
    app.Logger.LogInformation($"{conditions}");
    return TypedResults.Json(
          parameService.GetParames(dataSource, fields, conditions),
              new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = false });
});
app.Run();


public interface IParameService
{
    IList<dynamic> GetParames(string dataSource, string fields, string conditions);
}
public class ParameService : IParameService
{
    private readonly ILogger<ParameService> _logger;
    public ParameService(ILogger<ParameService> logger)
    {
        _logger = logger;
    }
    public IList<dynamic> GetParames(string dataSource, string fields, string conditions)
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

        var sql = $"select {fields} from {dataSource}";
        if (conditions != null && conditions.Length > 0)
        {
            var whereBuilder = new StringBuilder(" where ");
            var conditionArr = conditions.Split(new string[] { "(", "),(", ")" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var condition in conditionArr)
            {
                var arr = condition.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                whereBuilder.Append($" {arr[0]} {arr[1]} '{arr[2]}' and");
            }
            sql += whereBuilder.ToString().Substring(0, whereBuilder.Length - 3);
        }
        _logger.LogInformation(sql);
        return dir[dataSource.ToLower()];


    }
}