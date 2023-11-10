
using System.Net;
using System.Net.Sockets;
using System.Text;




Console.Title = "服务端";
var server = new TcpListener(IPAddress.Parse("127.0.0.1"), 7788);
server.Start();
Console.WriteLine("服务端已经启动");


var client = server.AcceptTcpClient();
Console.WriteLine("客户端已经连接");
var stream = client.GetStream();
var buffer = new byte[1024];

Task.Run(() =>
{
    while (true)
    {
        var length = stream.Read(buffer, 0, buffer.Length);
        var message = Encoding.UTF8.GetString(buffer, 0, length);
        Console.WriteLine($"接收到客户端消息：{message}");
    }
});

while (true)
{

    var content = Console.ReadLine();
    Console.WriteLine($"服务端已经发送消息：{content}");
    var response = Encoding.UTF8.GetBytes(content);
    stream.Write(response, 0, response.Length);
}


