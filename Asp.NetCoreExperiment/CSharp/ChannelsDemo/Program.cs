using System.Threading.Channels;

var chanel = Channel.CreateBounded<Data>(8);

while (true)
{
    chanel.Writer.TryWrite(new Data() { ID = 1, Name = "abcde", CreateOn = DateTime.Now });
    chanel.Writer.TryWrite(new Data() { ID = 2, Name = "ddddd", CreateOn = DateTime.Now });

    var result = chanel.Reader.TryRead(out Data? data);
    do
    {
        Console.WriteLine(data?.CreateOn.Second);
        Console.WriteLine(data?.CreateOn.Millisecond);
        Console.WriteLine(data?.CreateOn.Microsecond);
        Console.WriteLine(data?.CreateOn.Nanosecond);
        Console.WriteLine(data);
        result = chanel.Reader.TryRead(out data);

    } while (result);
    Console.ReadLine();
}

record Data
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public DateTime CreateOn { get; set; }
}