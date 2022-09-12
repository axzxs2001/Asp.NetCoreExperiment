using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Web;

var builder = WebApplication.CreateBuilder(args);


var connection = new SqliteConnection("Data Source=InMemorySample;Mode=Memory;Cache=Shared");

await InitDataAsync();
async Task InitDataAsync()
{
    await connection.OpenAsync();

    await connection.ExecuteAsync("create table type(ID INTEGER primary key,Name text,Category INTEGER);");
    await connection.ExecuteAsync("insert into main.type(ID,Name,Category) values(1,'AliPay',1),(2,'MiroPay',1),(3,'PayPay',2);");

    await connection.ExecuteAsync("CREATE TABLE city ([Key] INTEGER PRIMARY KEY,[Value] TEXT, [Type] INTEGER);");
    await connection.ExecuteAsync("insert into city(Key,Value,Type) values(1,'北京',1),(2,'上海',1),(3,'东京',2);");

    await connection.ExecuteAsync("create table [order](ID INTEGER primary key,Name text,Price real,Quantity integer);");
    await connection.ExecuteAsync("insert into [order](ID,Name,Price,Quantity) values(1,'产品A',21.15,12),(2,'产品B',32.45,23),(3,'产品C',43.45,23);");

}
builder.Services.AddSingleton<IDbConnection>(connection);


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


app.MapGet("/parame/{dataSource}", async (IParameService parameService, string dataSource, string fields, string conditions) =>
{
    var list = await parameService.GetParamesAsync(dataSource, fields, conditions);
    return TypedResults.Json(list, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = false });
});
app.Run();


public interface IParameService
{
    Task<IEnumerable<dynamic>> GetParamesAsync(string dataSource, string fields, string conditions);
}
public class ParameService : IParameService
{
    private readonly ILogger<ParameService> _logger;
    private readonly IDbConnection _db;
    public ParameService(ILogger<ParameService> logger, IDbConnection db)
    {
        _logger = logger;
        _db = db;
    }
    public async Task<IEnumerable<dynamic>> GetParamesAsync(string dataSource, string fields, string conditions)
    {
        //var dir = new Dictionary<string, List<dynamic>>()
        //{
        //    {
        //        "type",
        //        new List<dynamic>
        //        {
        //            new { ID=1,Name="AliPay"},
        //            new { ID=2,Name="MiroPay"},
        //            new { ID=3,Name="PayPay"}
        //        }
        //    },
        //    {
        //        "city",
        //        new List<dynamic>
        //        {
        //            new { Key=1,Value="BeiJing"},
        //            new { Key=2,Value="ShangHai"},
        //            new { Key=3,Value="ShenZhen"}
        //        }

        //    },
        //    {
        //        "order",
        //         new List<dynamic>
        //        {
        //            new { ID=1,Name="AAAA",Price=11.1m,Quantity=10},
        //            new { ID=2,Name="BBBB",Price=12.2m,Quantity=12},
        //            new { ID=3,Name="CCCC",Price=13.3m,Quantity=13},
        //        }
        //    }
        //};

        var sql = $"select {fields} from [{dataSource}]";
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
        return await _db.QueryAsync<dynamic>(sql);
        //return dir[dataSource.ToLower()];

    }
}