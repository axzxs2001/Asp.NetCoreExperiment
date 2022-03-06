var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.Urls.Add("https://*:7890");

app.MapGet("/line", (string code, string state) =>
{
    Console.WriteLine(code);
    Console.WriteLine(state);
});

app.Run();

