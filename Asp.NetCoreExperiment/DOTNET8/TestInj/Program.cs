using Dapper;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Concurrent;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

var conn = new MySqlConnection(@"");
var list = conn.Query<CaseTest>("SELECT * FROM test_case;").ToList();
builder.Services.AddScoped<IDbConnection, MySqlConnection>(pro => conn);
builder.Services.AddSingleton(list);
var app = builder.Build();

app.UseMiddleware<MiddlewareTest>();

app.MapGet("/weatherforecast", (List<CaseTest> list) =>
{
    return list;
});

app.Run();
class MiddlewareTest
{
    RequestDelegate _next;
    public MiddlewareTest(RequestDelegate next)
    {
        _next = next;
    }
   
    public async Task InvokeAsync(HttpContext context, List<CaseTest> list, IServiceProvider serPro)
    {       
        foreach (var property in serPro.GetType().GetProperties(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance))
        {
            if (property.Name == "RootProvider")
            {
                var pv = property.GetValue(serPro);
                foreach (var field in pv.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance))
                {
                  
                    if (field.Name == "_realizedServices")
                    {                       
                        var fv = field.GetValue(pv) as IEnumerable;                   
                        foreach (var item in fv)
                        {
                            Console.WriteLine(item );
                        }
                    }
                }

            }

        }


        await context.Response.WriteAsync("MiddlewareTest");

    }

}

class CaseTest
{
    public string Name { get; set; }
    public int Plan_ID { get; set; }
}