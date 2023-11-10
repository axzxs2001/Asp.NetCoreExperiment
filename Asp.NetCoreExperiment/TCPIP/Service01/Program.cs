using System.Net;
using System.Net.Sockets;
using System.Text;

Console.Title = "服务端";

var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000));
socket.Listen(10);
Console.WriteLine("等待连接...");
while (true)
{
    // 接受连接请求
    Socket clientSocket = socket.Accept();
    Console.WriteLine("客户端已连接");

    // 处理连接的客户端
    HandleClient(clientSocket);
}

static void HandleClient(Socket clientSocket)
{
    // 为每个客户端创建一个新线程来处理通信
    // 也可以使用异步方法进行处理
    byte[] buffer = new byte[1024];
    int bytesRead;

    try
    {
        while (true)
        {
            // 接收数据
            bytesRead = clientSocket.Receive(buffer);
            if (bytesRead == 0)
                break;

            // 处理接收到的数据
            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"接收到数据: {receivedData}");

            // 发送响应数据
            string responseData = "收到消息";
            byte[] responseBytes = Encoding.UTF8.GetBytes(responseData);
            clientSocket.Send(responseBytes);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
    }
    finally
    {
        // 关闭客户端Socket
        clientSocket.Close();
        Console.WriteLine("客户端已断开连接");
    }
}
//var server=new TcpListener(IPAddress.Parse("127.0.0.1"), 20000);

Console.ReadLine();
