

using System.ComponentModel.DataAnnotations;

//var order_00_a = new Order_00();
//order_00_a.Id = Guid.NewGuid();
//Console.WriteLine(order_00_a);

//var order_00_b = new Order_00 { Id = Guid.NewGuid() };
//Console.WriteLine(order_00_a);




//var order_01_a = new Order_01();
////order_01_a.Id = Guid.NewGuid();
//Console.WriteLine(order_01_a);


//var order_02_a = new Order_02();
////order_02_a.Id = Guid.NewGuid();
//Console.WriteLine(order_02_a);

//var order_02_b = new Order_02 { Id = Guid.NewGuid() };
//Console.WriteLine(order_02_b);



//var order_0N_a = new Order_03();

var order_0N_b = new Order_0N { Id = Guid.NewGuid(), OrderNo = "N2022098813594" };
Console.WriteLine(order_0N_b);


var orderType = typeof(Order_0N);

var orderObj = orderType.GetConstructor(new Type[0])?.Invoke(null);

var idPro = orderType.GetProperty("Id");
idPro?.SetValue(orderObj, Guid.NewGuid());

var noPro = orderType.GetProperty("OrderNo");
noPro?.SetValue(orderObj, "N12345678");


Console.WriteLine(orderObj);


Console.ReadLine();

public record Order_00
{
    public Guid Id { get; set; }
    private string? _orderNo;
    public string? OrderNo
    {
        get => _orderNo;
        set
        {
            if (!string.IsNullOrEmpty(value) && value.Length >= 8)
            {
                _orderNo = value;
            }
            else
            {
                throw new ApplicationException("OrderNo is error");
            }
        }
    }
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
    public void SetOrderNo(string no)
    {
        OrderNo = no;
    }
    public required Guid Id { get; init; } = Guid.NewGuid();
    public required string? OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
}