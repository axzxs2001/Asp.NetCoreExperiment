using Microsoft.AspNetCore.Http.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var app = builder.Build();

app.MapGet("/person/{id}",
    ([Range(1, 10000, ErrorMessage = "id的范围在1~10000之间")] int id)
        => TypedResults.Ok(id)).DisableValidation();

app.MapPost("/person", ([Required(ErrorMessage = "姓名不能为空")] string name, [Range(0, 120, ErrorMessage = "年龄应该在0~120之间")] int age) =>
TypedResults.Ok($"{name},{age}"));


app.MapPost("/order", ([FromBody] Order order) =>
TypedResults.Ok($"{order.OrderNo},{order.Amount}"));

app.Run();

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
public class Order
{
    [Required(ErrorMessage = "订单号不能为空")]
    public string OrderNo { get; set; }

    [Range(1d, double.MaxValue, ErrorMessage = "金额必需大于0")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "订单用户必须填写")]
    public User OrderUser { get; set; }
}

public class User
{
    [Range(1, int.MaxValue, ErrorMessage = "id大于0")]
    public int UserID { get; set; }
    [Required(ErrorMessage = "用户名不能为空")]
    public string UserName { get; set; }
}