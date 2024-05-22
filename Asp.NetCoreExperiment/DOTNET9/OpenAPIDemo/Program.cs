using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi("myapi", opt =>
{
    opt.UseTransformer((oper, context, c) =>
    {
        oper.Info = new OpenApiInfo
        {
            Version = "v1",
            Title = "My API",
            Description = "My API Description",
            Contact = new OpenApiContact
            {
                Name = "My Name",
                Email = "aaa@aa.com"
            },
            License = new OpenApiLicense
            {
                Name = "MIT",
                Url = new Uri("https://opensource.org/licenses/MIT")
            },
            TermsOfService = new Uri("https://www.google.com"),
        };
        return Task.CompletedTask;
    });
    opt.ShouldInclude = (o) =>
    {
        Console.WriteLine("-----HttpMethod------" + o.HttpMethod);
        Console.WriteLine("-----GroupName------" + o.GroupName);
        Console.WriteLine("------RelativePath-----" + o.RelativePath);
        Console.WriteLine("------ActionDescriptor-----" + o.ActionDescriptor.DisplayName);
        return true;
    };
    opt.UseOperationTransformer((oper, context, c) =>
    {
        Console.WriteLine("======summary=======" + oper.Summary);
        oper.Summary = "gui su wei test summary";
        oper.Tags = new OpenApiTag[]
        {
            new OpenApiTag
            {
                Name = "tag1", Description = "tag1 description",
            },
            new OpenApiTag
            {
                Name = "tag2", Description = "tag2 description",
            }
        };
        return Task.CompletedTask;
    });

});
var app = builder.Build();
app.MapOpenApi("/openapi/{documentName}.json");

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