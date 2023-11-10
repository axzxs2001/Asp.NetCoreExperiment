
using System.Text;
using System.Net;
using System.Net.Sockets;

Console.Title = "客户端";
Console.WriteLine("回车开始连接服务端");
Console.ReadLine();
var client = new TcpClient();
await client.ConnectAsync(IPAddress.Parse("127.0.0.1"), 7788);
Console.WriteLine("客户端已经连接");
var stream = client.GetStream();
var buffer = new byte[1024];

Task.Run(() =>
{
    while (true)
    {
        var length = stream.Read(buffer, 0, buffer.Length);
        var response = Encoding.UTF8.GetString(buffer, 0, length);
        Console.WriteLine($"接收到服务端消息：{response}");
    }

});

while (true)
{
    var content = Console.ReadLine();
    Console.WriteLine($"客户端已经发送消息：{content}");
    var message = Encoding.UTF8.GetBytes(content);
    stream.Write(message, 0, message.Length);

}


