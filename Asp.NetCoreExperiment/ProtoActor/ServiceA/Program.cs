using Proto;
using Proto.Remote;
using Proto.Serialization.Wire;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "服务端";          
            var wire = new WireSerializer(new[] { typeof(Ping), typeof(Pong), typeof(StartRemote), typeof(Start) });
            Serialization.RegisterSerializer(wire, true);

            Remote.Start("127.0.0.1", 12001);
            var messageCount = 1000000;
            var wg = new AutoResetEvent(false);
            var props = Actor.FromProducer(() => new LocalActor(0, messageCount, wg));

            var pid = Actor.Spawn(props);
            var remote = new PID("127.0.0.1:12000", "remote");
            remote.RequestAsync<Start>(new StartRemote { Sender = pid }).Wait();

            var start = DateTime.Now;
            Console.WriteLine("Starting to send");
            var msg = new Ping();
            for (var i = 0; i < messageCount; i++)
            {
                remote.SendSystemMessage(msg);
            }
            wg.WaitOne();
            var elapsed = DateTime.Now - start;
            Console.WriteLine("Elapsed {0}", elapsed);

            var t = messageCount * 2.0 / elapsed.TotalMilliseconds * 1000;
            Console.WriteLine("Throughput {0} msg / sec", t);

            Console.ReadLine();
        }

        public class LocalActor : IActor
        {
            private int _count;
            private readonly int _messageCount;
            private readonly AutoResetEvent _wg;

            public LocalActor(int count, int messageCount, AutoResetEvent wg)
            {
                _count = count;
                _messageCount = messageCount;
                _wg = wg;
            }

            public Task ReceiveAsync(IContext context)
            {
                switch (context.Message)
                {
                    case Pong _:
                        _count++;
                        if (_count % 50000 == 0)
                        {
                            Console.WriteLine(_count);
                        }
                        if (_count == _messageCount)
                        {
                            _wg.Set();
                        }
                        break;
                }
                return Actor.Done;
            }
        }

        public class Ping
        {
        }

        public class Pong
        {
        }

        public class Start
        {
        }

        public class StartRemote
        {
            public PID Sender { get; set; }
        }
    }

}
