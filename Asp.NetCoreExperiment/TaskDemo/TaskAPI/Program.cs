using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var logger = app.Logger;
app.MapGet("/testweb", (int second = 10, bool isexception = false) =>
{
    logger.LogInformation("/testweb������");
    Thread.Sleep(10000);
    if (isexception)
    {
        throw new Exception("�쳣����");
        }
    else
    {
        return Results.Ok("���web����");
    }
});
app.Run();

