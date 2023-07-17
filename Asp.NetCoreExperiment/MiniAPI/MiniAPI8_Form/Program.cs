using Microsoft.AspNetCore.Mvc;
using MiniAPI8_Form;

var builder = WebApplication.CreateSlimBuilder(args);

var app = builder.Build();

var sampleTodos = TodoGenerator.GenerateTodos().ToList();

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

todosApi.MapPost("/", ([FromForm] Todo todo) =>
{
    sampleTodos.Add(todo);
});

app.Run();

