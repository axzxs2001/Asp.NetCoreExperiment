using System.Net;
using System.Net.Sockets;

Console.Title = "客户端";
Console.ReadLine();

var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
await socket.ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000));

await socket.SendAsync(System.Text.Encoding.UTF8.GetBytes("你好"));

//var server=new TcpListener(IPAddress.Parse("127.0.0.1"), 20000);

Console.ReadLine();
