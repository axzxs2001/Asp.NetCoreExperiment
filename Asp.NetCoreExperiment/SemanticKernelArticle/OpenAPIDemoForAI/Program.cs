using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
var app = builder.Build();
app.MapOpenApi();
app.MapGet("/orders", () =>
{
    app.Logger.LogInformation("����orders");
    var orders = Enumerable.Range(1, 5).Select(index =>
        new Order(Guid.NewGuid().ToString(),$"Product {index}",index,index * 10))
        .ToArray();
    return orders;
})
.WithName("orders").WithDescription("��ȡ�����б�");
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
    [JsonPropertyName("���")]
    public string Id { get; set; }
    [JsonPropertyName("��Ʒ����")]
    public string Product { get; set; }
    [JsonPropertyName("��������")]
    public int Quantity { get; set; }
    [JsonPropertyName("�������")]
    public decimal Price { get; set; }
}