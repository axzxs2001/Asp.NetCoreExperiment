using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/test", () =>
{
})
.WithTags("TestApi");

app.MapPost("/test", () =>
{
})
.WithTags("TestApi");


app.MapGroup("/data").MapDataApi();//.RequireAuthorization();

app.Run();

/// <summary>
/// 分组扩展类
/// </summary>
public static class DataApi
{
    public static GroupRouteBuilder MapDataApi(this GroupRouteBuilder group)
    {
        group.MapGet("/", Query);
        group.MapDelete("/{id}", Remove);
        group.MapPost("/", Add);
        group.MapPut("/", Modify).WithOpenApi();
        return group;
    }
    /// <summary>
    /// 查询数据
    /// </summary>
    /// <returns></returns>
    public static async Task<JsonHttpResult<List<Data>>> Query()
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1));
        return TypedResults.Json<List<Data>>(new List<Data> {
            new Data { ID = 1, Name = "test01" },
            new Data { ID = 2, Name = "test02" },
        });
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static async Task<Ok> Remove(int id)
    {
        await Task.Delay(1);
        return TypedResults.Ok();
    }
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static async Task<Created<string>> Add(Data data)
    {
        await Task.Delay(1);
        return TypedResults.Created<string>("/", "ok");
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static async Task<Ok> Modify(Data data)
    {
        await Task.Delay(1);
        return TypedResults.Ok();
    }
}

public class Data
{
    public int ID { get; set; }
    public string? Name { get; set; }
}