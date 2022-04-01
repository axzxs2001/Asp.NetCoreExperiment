var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/test", () =>
{
    return new Order
    {
        Id = 1,
        Name = "ÕÅÈı",
        Created = DateTime.Now,
        Price = 100.34m
    };
});

app.Run();



class Order
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public DateTime Created { get; set; }
}