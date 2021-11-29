var builder = WebApplication.CreateBuilder();

Console.WriteLine(DateTime .Now);
Console.WriteLine(builder.Configuration.GetConnectionString("Exam"));
var app = builder.Build();

app.MapGet("/", () => "Hello .NET Mini API...?");
app.Run();