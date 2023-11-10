using System.Net;
using System.Net.Sockets;
using System.Text;

Console.Title = "客户端";
Console.WriteLine("回车后开始运行");
Console.ReadLine();
var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
await socket.ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000));
await socket.SendAsync(System.Text.Encoding.UTF8.GetBytes($"你好:{DateTime.Now}"));
var buffer = new byte[1024];
// 接收数据
var bytesRead = await socket.ReceiveAsync(buffer);
// 处理接收到的数据
var receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
Console.WriteLine($"接收到数据: {receivedData}");


Console.ReadLine();
