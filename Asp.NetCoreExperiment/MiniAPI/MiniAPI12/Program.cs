var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();


app.MapGet("/test", () =>
{    
});

app.Run();
