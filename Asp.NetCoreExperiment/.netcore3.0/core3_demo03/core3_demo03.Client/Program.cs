using Greet;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using Test;

namespace core3_demo03.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.ReadLine();
            var port = args.Length > 0 ? args[0] : "8501";

            var channel = new Channel("localhost:"+port, ChannelCredentials.Insecure);
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(
                                          new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);

            await channel.ShutdownAsync();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
