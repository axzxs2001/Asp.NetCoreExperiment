var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();



app.MapGet("/test", () => {
    return "ok";
});

app.Run();

