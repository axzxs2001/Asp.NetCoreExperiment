


using System.Collections.Generic;

var (no1, orderTime, _) = new Order1("T000001", DateTime.Now, "北京市海淀区");
Console.WriteLine(no1);
Console.WriteLine(orderTime);

Console.ReadLine();

(string no, List<int> goodsIds) = new Order("T000001", DateTime.Now, "北京市海淀区")
{
    Goodses = new List<Goods>
    {
        new Goods { ID = 1, Name = "商品A", Price = 10m },
        new Goods { ID = 2, Name = "商品B", Price = 15m },
    }
};
Console.WriteLine($"OrderNo：{no}");
foreach (var goodsId in goodsIds)
{
    Console.WriteLine($"    GoodsId：{goodsId}");
}


try
{
    throw new Exception("1级错误", new Exception("2级错误"));
}
catch (Exception exc)
{
    var (msg, (innerMsg, _)) = exc;
    Console.WriteLine(msg);
    Console.WriteLine(innerMsg);
}

record Order1(string No, DateTime OrderTime, string Address);


class Order
{
    public Order(string no, DateTime orderTime, string address)
    {
        No = no;
        OrderTime = orderTime;
        Address = address;
    }
    public string No { get; set; }
    public DateTime OrderTime { get; set; }
    public string Address { get; set; }
    public List<Goods> Goodses { get; set; } = new List<Goods>();

    public void Deconstruct(out string no, out List<int> goodsIds)
    {
        no = No;
        goodsIds = Goodses.Select(a => a.ID).ToList();
    }
    public void Deconstruct(out string no, out DateTime orderTime, out string address)
    {
        no = No;
        orderTime = OrderTime;
        address = Address;
    }
    public void Deconstruct(out string no, out DateTime orderTime)
    {
        no = No;
        orderTime = OrderTime;
    }
}
class Goods
{
    public int ID { get; set; }
    public string Name { get; set; }

    public decimal Price { get; set; }

}


static class ExceptionExtensions
{
    public static void Deconstruct(this Exception? exception, out string? message, out Exception? innerException)
    {
        message = exception?.Message;
        innerException = exception?.InnerException;
    }

    public static void Deconstruct(this Exception? exception, out string? message, out string? innerMessage, out Exception? innerInnerException)
    {
        message = exception?.Message;
        innerMessage = exception?.InnerException?.Message;
        innerInnerException = exception?.InnerException?.InnerException;
    }
}