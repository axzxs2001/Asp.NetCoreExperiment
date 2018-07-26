using Messages;
using Proto;
using Proto.Remote;
using System;
using ProtosReflection = Messages.ProtosReflection;
namespace ProtoRemote01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "服务端";
            Console.WriteLine("回车开始");
            Console.ReadLine();

            Serialization.RegisterFileDescriptor(ProtosReflection.Descriptor);
            var props = Actor.FromFunc(ctx =>
            {
                switch (ctx.Message)
                {
                    case HelloRequest msg:
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


    //public class HelloRequest
    //{
    //    public string Message
    //    {
    //        get; set;
    //    }
    //}

    //public class HelloResponse
    //{
    //    public string Message
    //    { get; set; }
    //}
}
