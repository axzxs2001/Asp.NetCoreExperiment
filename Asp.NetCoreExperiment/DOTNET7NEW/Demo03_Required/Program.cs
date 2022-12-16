

var order_00_a = new Order_00();
order_00_a.Id = Guid.NewGuid();
Console.WriteLine(order_00_a);

var order_00_b = new Order_00 { Id = Guid.NewGuid() };
Console.WriteLine(order_00_a);




var order_01_a = new Order_01();
//order_01_a.Id = Guid.NewGuid();
Console.WriteLine(order_01_a);


var order_02_a = new Order_02();
//order_02_a.Id = Guid.NewGuid();
Console.WriteLine(order_02_a);

var order_02_b = new Order_02 { Id = Guid.NewGuid() };
Console.WriteLine(order_02_b);



//var order_0N_a = new Order_03();

var order_0N_b = new Order_0N { Id = Guid.NewGuid() };
Console.WriteLine(order_0N_b);


public record Order_00
{
    public Guid Id { get; set; }
    public string? OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
}

public record Order_01
{
    public Order_01()
    {
        Id = Guid.NewGuid();
    }
    public void SetID(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string? OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
}

public record Order_02
{
    public Order_02()
    {
        Id = Guid.NewGuid();
    }
    //public void SetID(Guid id)
    //{
    //    Id = id;
    //}
    public Guid Id { get; init; } = Guid.NewGuid();
    public string? OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
}


public record Order_03
{
    public Order_03()
    {
        Id = Guid.NewGuid();
    }
    //public void SetID(Guid id)
    //{
    //    Id = id;
    //}
    public Guid Id { get; init; }
    public string? OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
}



public record Order_0N
{
    public Order_0N()
    {
        Id = Guid.NewGuid();
    }
    //public void SetID(Guid id)
    //{
    //    Id = id;
    //}
    public required Guid Id { get; init; }
    public string? OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
}