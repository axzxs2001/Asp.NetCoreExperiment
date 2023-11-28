using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddScoped<ITODOService, TODOService>();
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
todosApi.MapGet("/", (ITODOService service) =>
{ 
    return service.GetTodos();
});
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
[JsonSerializable(typeof(Todo))]

internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}

interface ITODOService
{
    Todo[] GetTodos();
    Todo[] GetTodos1();
}


class TODOService : ITODOService
{


    public Todo[] GetTodos1()
    {
        using (var connection = new SqliteConnection($"Data Source={Directory.GetCurrentDirectory()}/todo.db"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"select * from todos";
            using (var reader = command.ExecuteReader())
            {
                var table = new DataTable();
                table.Load(reader);
                var todos = new List<Todo>();

                foreach (DataRow row in table.Rows)
                {
                    var todo = new Todo(default(int), null, null, false);
                    foreach (DataColumn col in table.Columns)
                    {
                    }
                }
                table.Rows[0].ItemArray[0].ToString();
                return todos.ToArray();               
            }
        }
    }


    public Todo[] GetTodos()
    {
        using (var connection = new SqliteConnection($"Data Source={Directory.GetCurrentDirectory()}/todo.db"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"select * from todos";
            using (var reader = command.ExecuteReader())
            {
                var todos = new List<Todo>();
                while (reader.Read())
                {
                    var name = reader.GetString(0);
                    todos.Add(new Todo(
                     GetValue<Int32>(reader.GetValue("Id")),
                     reader.GetValue("Title").ToString(),
                     GetValue<DateOnly>(reader.GetValue("DueBy")),
                     GetValue<bool>(reader.GetValue("IsComplete"))
                     ));
                }
                return todos.ToArray();
            }
        }
    }

    T GetValue<T>(object value) where T : struct, IParsable<T>
    {
        if (value == null)
        {
            return default;
        }
        else
        {
            return T.Parse(value!.ToString()!, null);
        }
    }
}