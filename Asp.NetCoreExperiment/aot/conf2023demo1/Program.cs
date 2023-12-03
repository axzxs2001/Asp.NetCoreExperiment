using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();
var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () =>
{
    //typeof(Todo).GetMembers();
    return GetPros(new Todo());
});
string[] GetPros<T>(T t)
{
    return typeof(T).GetProperties().Select(x => x.Name).ToArray();
}
app.Run();

public class Todo
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateOnly? DueBy { get; set; } = null;
    public bool IsComplete { get; set; } = false;
}

[JsonSerializable(typeof(string[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
    public override int GetHashCode()
    {
        typeof(Todo).GetMembers();
        return base.GetHashCode();
    }
}

