using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddScoped<ITODOService, TODOService>();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();
var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", async (ITODOService service) =>
{
    return await service.GetTodos();
});
todosApi.MapPost("/", async (ITODOService service, Todo todo) =>
{
    return await service.AddTodo(todo);
});

app.Run();


public class Todo
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public DateTime DueBy { get; set; }
    public bool IsComplete { get; set; }
}

[JsonSerializable(typeof(Todo[]))]
[JsonSerializable(typeof(Todo))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
interface ITODOService
{
    Task<Todo[]> GetTodos();
    Task<bool> AddTodo(Todo todo);
}


class TODOService : ITODOService
{
    public async Task<Todo[]> GetTodos()
    {
        using (var connection = new SqliteConnection($"Data Source={Directory.GetCurrentDirectory()}/todo.db"))
        {
            return (await QueryAsync(connection, "select * from todos")).ToArray();
        }
    }
    public async Task<bool> AddTodo(Todo todo)
    {
        using (var connection = new SqliteConnection($"Data Source={Directory.GetCurrentDirectory()}/todo.db"))
        {
            var sql = "insert into todos(Title,DueBy,IsComplete) values(@Title,@DueBy,@IsComplete)";
            var result = await ExecuteAsync(connection, sql, todo);
            return result > 0;
        }
    }
    async Task<Todo[]> QueryAsync(SqliteConnection connection, string sql)
    {
        await connection.OpenAsync();
        var command = connection.CreateCommand();
        command.CommandText = sql;
        using (var reader = await command.ExecuteReaderAsync())
        {
            var todos = new List<Todo>();
            while (await reader.ReadAsync())
            {
                var todo = Activator.CreateInstance(typeof(Todo));
                foreach (var pro in typeof(Todo).GetProperties())
                {
                    Console.WriteLine("有了有了有了");
                    var value = Convert.ChangeType(reader.GetValue(pro.Name), pro.PropertyType);
                    pro.SetValue(todo, value);
                }
                todos.Add((Todo)todo!);
            }
            return todos.ToArray();
        }
    }
    async Task<int> ExecuteAsync(SqliteConnection connection, string sql, Todo todo)
    {
        await connection.OpenAsync();
        var command = connection.CreateCommand();
        command.CommandText = sql;

        foreach (var pro in todo.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            command.Parameters.AddWithValue($"@{pro.Name}", pro.GetValue(todo));
        }
        return await command.ExecuteNonQueryAsync();
    }
}


