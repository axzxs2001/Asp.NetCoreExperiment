using Proto;
using System;
using System.Threading.Tasks;

namespace ProtoDemo0101
{
    class Program
    {
        static void Main(string[] args)
        {
            var props = Actor.FromProducer(() => new B2CActor());
            var pid = Actor.SpawnNamed(props, "B2C");

            Console.ReadLine();
        }
    }

    public class B2CActor : IActor
    {
        public Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case Started started:
                    var props = Actor.FromProducer(() => new OrderActor());
                    context.SpawnNamed(props, "Order");
                    break;
                case OK ok:
                    Console.WriteLine(ok);
                    break;

            }
            return Actor.Done;
        }
    }

    public class OrderActor : IActor
    {
        public Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case Started started:
                    context.Parent.Tell(new OK() { Message="OrderActor OK"});
                    break;
            }
            return Actor.Done;
        }
    }

    public class OK
    {
        public string Message
        { get; set; }

        public override string ToString()
        {
            return $"OK.Message={Message}";
        }
    }

}
