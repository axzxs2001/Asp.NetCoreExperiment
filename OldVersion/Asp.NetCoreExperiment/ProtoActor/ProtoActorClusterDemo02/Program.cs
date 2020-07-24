using System;
using System.Threading;
using Proto;
using Proto.Cluster;
using Proto.Cluster.Consul;
using Proto.Remote;
using Proto.Serialization.Wire;
using ProtoActorClusterDemo01;

namespace Node2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "服务端";
          
            var wire = new WireSerializer(new[] { typeof(HelloRequest), typeof(HelloResponse) });
            Serialization.RegisterSerializer(wire, true);
            var props = Actor.FromFunc(ctx =>
            {
                switch (ctx.Message)
                {
                    case HelloRequest helloRequest:
                        Console.WriteLine("服务端接收到："+helloRequest.Message);
                        ctx.Respond(new HelloResponse
                        {
                            Message = "Hello from node 1"
                        });
                        break;
                }
                return Actor.Done;
            });

            var parsedArgs = ParseArgs(args);
            Remote.RegisterKnownKind("HelloKind", props);

        
            Cluster.Start("MyCluster", parsedArgs.ServerName, 12000, new ConsulProvider(new ConsulProviderOptions(), c => c.Address = new Uri("http://" + parsedArgs.ConsulUrl + ":8500/")));

            Thread.Sleep(Timeout.Infinite);
            Console.WriteLine("Shutting Down...");
            Cluster.Shutdown();
        }

        private static Node2Config ParseArgs(string[] args)
        {
            if (args.Length > 0)
            {
                return new Node2Config(args[0], args[1]);
            }
            return new Node2Config("127.0.0.1", "127.0.0.1");
        }

        class Node2Config
        {
            public string ServerName { get; }
            public string ConsulUrl { get; }
            public Node2Config(string serverName, string consulUrl)
            {
                ServerName = serverName;
                ConsulUrl = consulUrl;
            }
        }
    }
}