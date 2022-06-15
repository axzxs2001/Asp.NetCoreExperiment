using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.MapGet("/test", ([AsParameters] Order order) =>
{
    order.Logger?.LogInformation(order.UUID);
});

app.Run();



record struct Order 
{
    [FromHeader(Name = "X-UUID")]
    public string? UUID { get; set; }
    [FromQuery(Name = "no")]
    public int OrderNo { get; set; }
    public ILogger<Order>? Logger { get; set; }
}