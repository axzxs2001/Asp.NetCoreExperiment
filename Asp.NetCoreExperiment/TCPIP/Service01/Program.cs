using System.Net;
using System.Net.Sockets;
using System.Text;

Console.Title = "服务端";
var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000));
socket.Listen(10);
Console.WriteLine("等待连接...");

// 接受连接请求
var clientSocket = socket.Accept();
Console.WriteLine("客户端已连接");
// 为每个客户端创建一个新线程来处理通信
// 也可以使用异步方法进行处理
var buffer = new byte[1024];

// 接收数据
var bytesRead = await clientSocket.ReceiveAsync(buffer);

// 处理接收到的数据
var receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
Console.WriteLine($"接收到数据: {receivedData}{DateTime.Now}");

// 发送响应数据
var responseData = "收到消息";
var responseBytes = Encoding.UTF8.GetBytes(responseData);
await clientSocket.SendAsync(responseBytes);

Console.ReadLine();

