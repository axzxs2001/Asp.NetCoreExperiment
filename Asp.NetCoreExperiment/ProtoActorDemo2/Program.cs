using Proto;
using System;

namespace ProtoActorDemo2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请求方");
            var content = Console.ReadLine();
            Test1(content);
            Console.ReadKey();
        }
        static void Test1(string hey)
        {
            var props = Actor.FromFunc(ctx =>
            {
                if (ctx.Message is string)
                    ctx.Respond(hey);
                return Actor.Done;
            });
            var pid = Actor.Spawn(props);
            Console.WriteLine(pid.Id);
            var reply = pid.RequestAsync<object>("hello").Result;
            Console.WriteLine($"请求方:{reply}");
        }
    }
}
