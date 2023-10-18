
int i1 = default;
Console.WriteLine(i1);
int? i2 = null;
Console.WriteLine(i2 ?? 0);
Console.WriteLine(i2.HasValue ? i2.Value : 0);



string? s1 = string.Empty;
Console.WriteLine(s1.Length);
string? s2 = default;
Console.WriteLine((s2 ?? string.Empty).Length);

Console.WriteLine(string.Empty == "");//true
Console.WriteLine(string.Equals(string.Empty, ""));//true
Console.WriteLine(string.Empty == null);//false
Console.WriteLine(string.Equals(string.Empty, null));//false

string sss = null;
Console.WriteLine(sss.Length);

var orders1 = GetOrders1();
foreach (var order in orders1)
{
    Console.WriteLine(order);
}

var orders2 = GetOrders2();
var orders3 = GetOrders3();

var order1 = GetOrder1();
Console.WriteLine(order1?.ToString() ?? "无");
var order2 = GetOrder2();
Console.WriteLine(order2?.ToString() ?? "无");
Console.ReadLine();



Order? GetOrder1()
{
    return default(Order);
    //return null;
}
Order GetOrder2()
{
    return new Order(default, string.Empty, default);

}

IEnumerable<Order> GetOrders1()
{
    return Enumerable.Empty<Order>();
}


IList<Order> GetOrders2()
{
    return new List<Order>();
}

Order[] GetOrders3()
{
    // return new Order[0];
    return Array.Empty<Order>();
}


public record Order(int Id, string Name, string? Description = null);

