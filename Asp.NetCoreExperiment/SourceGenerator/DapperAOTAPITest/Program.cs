using DapperAOTAPITest.Model;
using DapperAOTAPITest.Respository;
using DapperAOTAPITest.Service;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddScoped<ITodoRespository, TodoRespository>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();


app.MapGet("/", (ITodoService service) =>
{
    try
    {
        return new RequestResult { Data = service.Query<Todo>()?.ToList(), Message = "" };
    }
    catch (Exception ex)
    {
        return new RequestResult { Data = new List<Todo>(), Message = ex.Message };
    }
});

app.Run();

[JsonSerializable(typeof(RequestResult))]
[JsonSerializable(typeof(IEnumerable<Todo>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}

