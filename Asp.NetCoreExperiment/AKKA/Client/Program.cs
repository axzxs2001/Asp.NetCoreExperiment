using Akka.Actor;
using Akka.Configuration;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client";
            var config = ConfigurationFactory.ParseString(@"
akka {  
    actor {
        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
    }
    remote {
        helios.tcp {
            transport-class = ""Akka.Remote.Transport.Helios.HeliosTcpTransport, Akka.Remote""
            applied-adapters = []
            transport-protocol = tcp
            port = 0
            hostname = localhost
        }
    }
}
");
       
            using (var system = ActorSystem.Create("MyClient", config))
            {
                var greeting = system.ActorSelection("akka.tcp://MyServer@localhost:8081/user/Greeting");
                while (true)
                {
                    var input = Console.ReadLine();
                    greeting.Tell(new SayHellowMessage() { Name = "桂素伟", Content = $"这是一个测试，输入内容：{input}" });
                }
            }
        }
    }
}
