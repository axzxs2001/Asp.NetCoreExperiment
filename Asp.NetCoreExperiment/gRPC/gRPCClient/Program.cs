using Grpc.Net.Client;
using GrpcService1;

namespace gRPCClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7162");

            var client = new IToDoService.IToDoServiceClient(channel);

            while (true)
            {
                Console.WriteLine("1、add   2、remvoe  3、query all");
                switch (Console.ReadLine())
                {
                    case "1":
                        var addResult = await client.AddToDoAsync(new AddToDoRequest
                        {
                            ToDo = new ToDo
                            {
                                Title = "gsw",
                                Message = "add gsw",
                                CreateOn = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)
                            }
                        });
                        Console.WriteLine(addResult.Result);
                        break;
                    case "2":
                        var removeResult = await client.RemoveToDoAsync(new RemoveToDoRequest
                        {
                            Title = "gsw"
                        });
                        Console.WriteLine(removeResult.Result);
                        break;
                    case "3":
                        var queryToDoReply = await client.QueryToDoAsync(new Google.Protobuf.WellKnownTypes.Empty());
                        foreach (var todo in queryToDoReply.ToDos)
                        {
                            Console.WriteLine($"title:{todo.Title},message:{todo.Message},createon:{todo.CreateOn}");
                        }
                        break;
                }
            }
        }
    }
}