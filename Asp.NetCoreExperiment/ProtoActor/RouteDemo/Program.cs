using Proto;
using Proto.Router;
using System;
using System.Threading.Tasks;

namespace RouteDemo
{
    internal class Message : IHashable
    {
        public string Text;

        public string HashBy()
        {
            return Text;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    internal class MyActor : IActor
    {
        public Task ReceiveAsync(IContext context)
        {
            if (context.Message is Message msg)
            {
                Console.WriteLine($"Actor {context.Self.Id} got message '{msg.Text}'.");
            }
            return Actor.Done;
        }
    }

    internal class Program
    {
        private static readonly Props MyActorProps = Actor.FromProducer(() => new MyActor());

        private static void Main()
        {
            //TestBroadcastGroup();
            //System.Threading.Thread.Sleep(3000);
            Console.WriteLine("------------------------------");
            TestBroadcastPool();

            //TestRandomPool();
            //TestRandomGroup();

            //TestRoundRobinPool();
            //TestRoundRobinGroup();

            //TestConsistentHashPool();
            //TestConsistentHashGroup();

            Console.ReadLine();
        }
        /// <summary>
        ///分别调用 
        /// </summary>

        private static void TestBroadcastGroup()
        {
            var props = Router.NewBroadcastGroup(
                Actor.Spawn(MyActorProps),
                Actor.Spawn(MyActorProps),
                Actor.Spawn(MyActorProps),

                Actor.Spawn(MyActorProps)
            );
            for (var i = 0; i < 10; i++)
            {
                var pid = Actor.Spawn(props);
                pid.Tell(new Message { Text = $"{i % 4}" });
            }
        }

        /// <summary>
        /// 五个相同的Actor同时调用起来
        /// </summary>
        private static void TestBroadcastPool()
        {
            var props = Router.NewBroadcastPool(MyActorProps, 2);
            var pid = Actor.Spawn(props);
            for (var i = 0; i < 10; i++)
            {
                pid.Tell(new Message { Text = $"{i % 4}" });
            }
        }

        private static void TestConsistentHashGroup()
        {
            var props = Router.NewConsistentHashGroup(
                Actor.Spawn(MyActorProps),
                Actor.Spawn(MyActorProps),
                Actor.Spawn(MyActorProps),
                Actor.Spawn(MyActorProps)
            );
            var pid = Actor.Spawn(props);
            for (var i = 0; i < 10; i++)
            {
                pid.Tell(new Message { Text = $"{i}" });
            }
        }

        private static void TestConsistentHashPool()
        {
            var props = Router.NewConsistentHashPool(MyActorProps, 5);
            var pid = Actor.Spawn(props);
            for (var i = 0; i < 10; i++)
            {
                pid.Tell(new Message { Text = $"{i % 4}" });
            }
        }

        private static void TestRoundRobinGroup()
        {
            var props = Router.NewRoundRobinGroup(
                Actor.Spawn(MyActorProps),
                Actor.Spawn(MyActorProps),
                Actor.Spawn(MyActorProps),
                Actor.Spawn(MyActorProps)
            );
            var pid = Actor.Spawn(props);
            for (var i = 0; i < 10; i++)
            {
                pid.Tell(new Message { Text = $"{i % 4}" });
            }
        }

        private static void TestRoundRobinPool()
        {
            var props = Router.NewRoundRobinPool(MyActorProps, 5);
            var pid = Actor.Spawn(props);
            for (var i = 0; i < 10; i++)
            {
                pid.Tell(new Message { Text = $"{i % 4}" });
            }
        }

        private static void TestRandomGroup()
        {
            var props = Router.NewRandomGroup(
                Actor.Spawn(MyActorProps),
                Actor.Spawn(MyActorProps),
                Actor.Spawn(MyActorProps),
                Actor.Spawn(MyActorProps)
            );
            var pid = Actor.Spawn(props);
            for (var i = 0; i < 10; i++)
            {
                pid.Tell(new Message { Text = $"{i % 4}" });
            }
        }

        private static void TestRandomPool()
        {
            var props = Router.NewRandomPool(MyActorProps, 5);
            var pid = Actor.Spawn(props);
            for (var i = 0; i < 10; i++)
            {
                pid.Tell(new Message { Text = $"{i % 4}" });
            }
        }
    }
}
