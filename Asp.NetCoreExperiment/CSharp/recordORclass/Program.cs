
var order1 = new Order(1, "张三", "苹果");
Console.WriteLine(order1);
var order2 = order1 with { Id = 2, Name = "李四" };
Console.WriteLine(order2);

order1.Deconstruct(out var id, out var name, out var description);
Console.WriteLine($"Id:{id}, Name:{name}, Description:{description}");

var goods1 = new Goods(1, "Apple", "苹果");


//Console.WriteLine(order1);
//var goods2 = goods1 with { Id = 2, Name = "Banana" };
//Console.WriteLine(goods2);

record Order(int Id, string Name, 
    string? Description = null)
{
    public void Print()
    {
        Console.WriteLine($"Id:{Id}, Name:{Name}, Description:{Description}");
    }
}
class Goods(int Id, string Name, string? Description = null)
{
    public int Id { get; } = Id;
    public string Name { get; } = Name;
    public string? Description { get; } = Description;
}

