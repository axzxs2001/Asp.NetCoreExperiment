
using Proto;
using Proto.Remote;
using Proto.Serialization.Wire;
using System;
using Proto.Serialization.Json;

namespace ProtoRemote01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "服务端";
            Console.WriteLine("回车开始");
            Console.ReadLine();
            var wire = new WireSerializer(new[] { typeof(HelloRequest), typeof(HelloResponse) });
            Serialization.RegisterSerializer(wire, true);
            //var json = new JsonSerializer();
            //Serialization.RegisterSerializer(json, true);
            var props = Actor.FromFunc(ctx =>
            {
                switch (ctx.Message)
                {
                    case HelloRequest msg:
                        Console.WriteLine(msg.Message);
                        ctx.Respond(new HelloResponse
                        {
                            Message = "回应：我是服务端",
                        });
                        break;
                    default:
                        break;
                }
                return Actor.Done;
            });
           
            Remote.RegisterKnownKind("hello", props);
            Remote.Start("127.0.0.1", 12000);
            Console.WriteLine("服务端开始……");
            Console.ReadLine();
        }
    }


    public class HelloRequest
    {
        public string Message
        {
            get; set;
        }
    }

    public class HelloResponse
    {
        public string Message
        { get; set; }
    }
}
