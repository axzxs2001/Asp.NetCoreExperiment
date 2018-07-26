using System;
using System.Threading.Tasks;
using Messages;
using Proto;
using Proto.Remote;
using Proto.Serialization.Wire;

namespace Node2
{
    public class EchoActor : IActor
    {
        private PID _sender;

        public Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case StartRemote sr:
                    Console.WriteLine("Starting");
                    _sender = sr.Sender;
                    context.Respond(new Start());
                    return Actor.Done;
                case Ping _:
                    context.Send(_sender, new Pong());
                    return Actor.Done;
                default:
                    return Actor.Done;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            //Registering "knownTypes" is not required, but improves performance as those messages
            //do not need to pass any typename manifest
            var wire = new WireSerializer(new[] { typeof(Ping), typeof(Pong), typeof(StartRemote), typeof(Start) });
            Serialization.RegisterSerializer(wire, true);
            Remote.Start("127.0.0.1", 12000);
            Remote.SpawnNamedAsync("127.0.0.1:12000", "remote", "test", TimeSpan.FromSeconds(5));
            //Remote.SpawnNamed(Actor.FromProducer(() => new EchoActor()), "remote");
            Console.ReadLine();
        }
    }


}