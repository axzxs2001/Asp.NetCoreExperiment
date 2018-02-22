using Proto;
using System;
using System.Threading.Tasks;

namespace ProtoActorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("接收方");
            Console.ReadKey();
            Test2();
           
            Console.ReadKey();
        }
        static void Test2()
        {
            var props = Actor.FromProducer(() => new HelloActor());
            var pid = Actor.Spawn(props);
            Console.WriteLine(pid.Id);
            pid.Tell(new Hello
            {
                Who = "Alex"
            });
        }
        //static void Test1()
        //{
        //    var props = Actor.FromFunc(ctx =>
        //    {
        //        if (ctx.Message is string)
        //            ctx.Respond("hey");
        //        return Actor.Done;
        //    });
        //    var pid = Actor.Spawn(props);

        //    var reply = pid.RequestAsync<object>("hello").Result;
        //    Console.WriteLine($"Test1:{reply}");
        //}
    }

    internal class Hello
    {
        public string Who;
    }
    internal class HelloActor : IActor
    {
        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            if (msg is Hello r)
            {
                Console.WriteLine($"ReceiveAsync:Hello {r.Who}");
            }
            return Actor.Done;
        }
    }
}
