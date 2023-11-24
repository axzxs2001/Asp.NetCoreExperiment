using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var logger = app.Logger;
app.MapGet("/testweb", () =>
{
    logger.LogInformation("/testweb被请求！");
    Thread.Sleep(10000);
    return Results.Ok("完成web请求");
});
app.Run();

