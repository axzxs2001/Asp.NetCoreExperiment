var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.UseStaticFiles();
app.MapGet("/test", () =>
{

});

app.Run();

