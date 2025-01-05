using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
var app = builder.Build();
app.MapOpenApi();
app.MapGet("/orders", () =>
{
    app.Logger.LogInformation("调用orders");
    var orders = Enumerable.Range(1, 5).Select(index =>
        new Order(Guid.NewGuid().ToString(),$"Product {index}",index,index * 10))
        .ToArray();
    return orders;
})
.WithName("orders").WithDescription("获取订单列表");
app.Run();

class Order
{
    public Order(string id, string product, int quantity, decimal price)
    {
        Id = id;
        Product = product;
        Quantity = quantity;
        Price = price;
    }
    [JsonPropertyName("编号")]
    public string Id { get; set; }
    [JsonPropertyName("产品名称")]
    public string Product { get; set; }
    [JsonPropertyName("订单数量")]
    public int Quantity { get; set; }
    [JsonPropertyName("订单金额")]
    public decimal Price { get; set; }
}