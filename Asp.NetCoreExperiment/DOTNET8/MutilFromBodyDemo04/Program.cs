using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();


app.MapPost("/todos", ([FromBody] Todo todo, [FromBody] User user) =>
{
    app.Logger.LogInformation(todo.ToString());
    app.Logger.LogInformation(user.ToString());
});

app.Run();


class Todo
{
    public string? Id { get; set; }
}
record User
{
    public string Name { get; set; }
}