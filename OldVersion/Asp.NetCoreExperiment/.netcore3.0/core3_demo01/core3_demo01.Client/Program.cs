using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test;
using Grpc.Core;

namespace core3_demo01
{
    public class Program
    {
        static async Task Main(string[] args)
        {

            var port = args.Length > 0 ? args[0] : "5001";

            var channel = new Channel("localhost:" + port, ChannelCredentials.Insecure);
            var client = new Test1.Test1Client(channel);

            var reply = await client.SendAsync(new Inn { Name = "GreeterClient", Key=10, });
            Console.WriteLine("Greeting: " + reply.Message);

            await channel.ShutdownAsync();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
