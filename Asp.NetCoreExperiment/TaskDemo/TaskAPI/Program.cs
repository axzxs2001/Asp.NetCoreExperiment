using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var logger = app.Logger;
app.MapGet("/testweb", (int second = 10, bool isexception = false) =>
{
    logger.LogInformation("/testweb被请求！");
    Thread.Sleep(10000);
    if (isexception)
    {
        throw new Exception("异常测试");
        }
    else
    {
        return Results.Ok("完成web请求");
    }
});
app.Run();

