using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Nodes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi("myapi", options =>
{
    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_1; 
});
var app = builder.Build();
//app.MapOpenApi("/openapi/{documentName}.json");
app.MapOpenApi();
app.MapGet("/scalar/{documentName}", (string documentName) => Results.Content($$"""
              <!doctype html>
              <html>
              <head>
                  <title>Scalar API Reference -- {{documentName}}</title>
                  <meta charset="utf-8" />
                  <meta
                  name="viewport"
                  content="width=device-width, initial-scale=1" />
              </head>
              <body>
                  <script
                  id="api-reference"
                  data-url="/openapi/{{documentName}}.json"></script>
                  <script>
                  var configuration = {
                      theme: 'purple',
                  }              
                  document.getElementById('api-reference').dataset.configuration =
                      JSON.stringify(configuration)
                  </script>
                  <script src="https://cdn.jsdelivr.net/npm/@scalar/api-reference"></script>
              </body>
              </html>
              """, "text/html")).ExcludeFromDescription();
app.MapGet("/order", () =>
{
    return new Order()
    {
        OrderNo = "20210901",
        Amount = 100,
        OrderDate = DateTime.Now
    };
});
app.MapPost("/order", (Order order) =>
{
    return new OkResult();
});
app.Run();

/// <summary>
/// 订单
/// </summary>
public class Order
{
    /// <summary>
    /// 订单编号
    /// </summary>
    public string OrderNo { get; set; }
    /// <summary>
    /// 订单金额
    /// </summary>
    public decimal Amount { get; set; }
    /// <summary>
    /// 订单日期
    /// </summary>
    public DateTime OrderDate { get; set; }

}