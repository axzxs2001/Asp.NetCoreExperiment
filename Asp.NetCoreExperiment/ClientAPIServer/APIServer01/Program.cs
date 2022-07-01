var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();



app.MapGet("/kv/{*path}", (string path) =>
{
    app.Logger.LogInformation($"{path}");

    return TypedResults.Json(
          new List<dynamic> { new { Name = "aaaa", ID = 1 }, new { Name = "bbbb",ID = 2} },
          new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = false });

});

app.Run();
