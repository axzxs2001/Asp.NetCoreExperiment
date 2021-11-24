using static System.Console;

Order? order = null;
PrintOrder(order);

static void PrintOrder(Order? order)
{
    WriteLine($"OrderNo:{order?.OrderNo},Amount:{order?.Amount},OrderTime:{order?.Amount}");

    //这还是会发空引用异常
    //foreach (var detail in order?.Details)
    //{
    //    WriteLine($"GoodsID:{detail.GoodsID},Quantity:{detail.Quantity},Price:{detail.Price}");
    //}
    //这样就不会引发空引用异常
    for (int i = 0; i < order?.Details?.Count; i++)
    {
        var detail = order.Details[i];
        WriteLine($"GoodsID:{detail.GoodsID},Quantity:{detail.Quantity},Price:{detail.Price}");
    }
}

public class Order
{
    public string? OrderNo { get; set; }
    public decimal Amount { get; set; }
    public DateTime OrderTime { get; set; }
    public List<OrderDetail>? Details { get; set; }
}

public class OrderDetail
{
    public string? GoodsID { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}