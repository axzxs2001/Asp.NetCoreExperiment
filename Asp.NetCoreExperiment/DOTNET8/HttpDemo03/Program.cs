using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
var logger = app.Logger;

app.MapGet("item/{id:int}", (int id) =>
{
    logger.LogInformation("按 ID={0} 查询Item", id);
    return new Item { ID = 1, Name = "PS5", Price = 2000, Describe = "White" };
});

app.MapPost("item", ([FromBody] Item item) =>
{
    logger.LogInformation("添加Item:{0}", item);
    item.ID = 999;
    return item;
});

app.MapDelete("item/{id:int}", (int id) =>
{
    logger.LogInformation("按 ID={0} 删除Item", id);
    return TypedResults.Ok("删除成功！");
});

app.MapPut("item", ([FromBody] Item item) =>
{
    logger.LogInformation("按 ID={0} 修改Item", item.ID);
    item.Describe = "White+Black";
    return item;
});

app.Run();


public record Item
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Describe { get; set; }
}