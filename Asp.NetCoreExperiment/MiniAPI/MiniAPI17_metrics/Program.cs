using Prometheus;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/test", () =>
{
    return "OK";
});
app.MapMetrics();
app.UseHttpMetrics();
app.Run();
