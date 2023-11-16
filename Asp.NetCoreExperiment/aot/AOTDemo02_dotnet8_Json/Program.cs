using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

var sampleTodos = new Todo[] {
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
};

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());


var ordersApi = app.MapGroup("/orders");
ordersApi.MapGet("/", () => new Order[]
{
    new(1, 10.5m, 2),
    new(2, 20.5m, 3),
    new(3, 30.5m, 4),
    new(4, 40.5m, 5),
    new(5, 50.5m, 6)
});

ordersApi.MapPost("/add", CreateTodo);

app.Run();

static async Task<bool> CreateTodo(Order order)
{    Console.WriteLine(order);
    return await Task.FromResult(true);
}

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

public record Order(int Id, decimal Amount, int Quantity);

[JsonSerializable(typeof(Todo[]))]
[JsonSerializable(typeof(Order[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
