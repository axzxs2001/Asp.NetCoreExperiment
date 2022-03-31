

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IOrderService, OrderService>();
var app = builder.Build();

app.MapGet("/order", (IOrderService orderService) =>
{
    return "Result:" + orderService.GetOrder("123");
});
app.MapPost("/order", (Order order, IOrderService orderService) =>
{
    return "Result:" + orderService.AddOrder(order);
});

app.Run();


//public class Program
//{
//    static void Main(string[] args)
//    {
//        var builder = WebApplication.CreateBuilder(args);
//        builder.Services.AddScoped<IOrderService, OrderService>();
//        var app = builder.Build();

//        app.MapGet("/test", (IOrderService orderService) =>
//        {
//            return "Result:" + orderService.GetOrder("123");
//        });
//        app.Run();
//    }

//}



public interface IOrderService
{
    bool AddOrder(Order order);
    string GetOrder(string orderNo);
}
public class OrderService : IOrderService
{
    private readonly ILogger<OrderService> _logger;
    public OrderService(ILogger<OrderService> logger)
    {
        _logger = logger;
    }
    public string GetOrder(string orderNo)
    {
        return "this is my order,orderno:" + orderNo;
    }

    public bool AddOrder(Order order)
    {
        _logger.LogInformation(order.ToString());
        return true;
    }
}

public record Order
{
    public string OrderNo { get; set; }
    public string Name { get; set; }

    public decimal Price { get; set; }
}