using Proto;
using Proto.Mailbox;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProtoActorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // HelloWord();
            Futures();
            //LifecycleEvents();
        }

        static void LifecycleEvents()
        {
            var props = Actor.FromProducer(() => new ChildActor());
            var actor = Actor.Spawn(props);
            actor.Tell(new Hello
            {
                Who = "Alex"
            });
            //why wait?
            //Stop is a system message and is not processed through the user message mailbox
            //thus, it will be handled _before_ any user message
            //we only do this to show the correct order of events in the console
            Thread.Sleep(TimeSpan.FromSeconds(1));
            actor.Stop();

            Console.ReadLine();
        }

        /// <summary>
        /// 请求应答，有参，有返回值
        /// </summary>
        static void Futures()
        {
            var props = Actor.FromFunc(context =>
            {
               
                var msg = context.Message;
                if (msg is Hello r)
                {
                    Console.WriteLine($"Hello {r.Who}");
                    context.Respond(new Hello() { Who = "应答" });
                }
                return Actor.Done;
            });
            var pid = Actor.Spawn(props);

            var reply = pid.RequestAsync<object>(new Hello { Who = "请求" }).Result;
            Console.WriteLine((reply as Hello).Who);
            Console.ReadLine();
        }

        /// <summary>
        /// 只请求
        /// </summary>
        static void HelloWord()
        {
            var props = Actor.FromProducer(() => new HelloActor());
            var pid = Actor.Spawn(props);
            pid.Tell(new Hello
            {
                Who = "ProtoActor"
            });
            Console.ReadLine();
        }


    }

    internal class ChildActor : IActor
    {
        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;

            switch (context.Message)
            {
                case Hello r:
                    Console.WriteLine($"Hello {r.Who}");
                    break;
                case Started r:
                    Console.WriteLine("Started, initialize actor here");
                    break;
                case Stopping r:
                    Console.WriteLine("Stopping, actor is about shut down");
                    break;
                case Stopped r:
                    Console.WriteLine("Stopped, actor and it's children are stopped");
                    break;
                case Restarting r:
                    Console.WriteLine("Restarting, actor is about restart");
                    break;
            }
            return Actor.Done;
        }
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
                Console.WriteLine($"Hello {r.Who}");
            }
            return Actor.Done;
        }
    }
}