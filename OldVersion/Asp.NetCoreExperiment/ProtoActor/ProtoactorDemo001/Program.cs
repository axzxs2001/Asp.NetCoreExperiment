using Proto;
using System;
using System.Threading.Tasks;

namespace ProtoactorDemo001
{
    public class TheActor : IActor
    {
        public Task ReceiveAsync(IContext ctx)
        {
            Console.WriteLine(ctx.Message);
            return Task.FromResult(0);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var props = Actor.FromProducer(() => new TheActor());
            var a = Actor.Spawn(props);
            a.Tell("Hello");
            Console.ReadLine();
        }
    }
}
