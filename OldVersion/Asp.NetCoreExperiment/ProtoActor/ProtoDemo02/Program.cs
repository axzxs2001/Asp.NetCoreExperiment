using Proto;
using Proto.Mailbox;
using System;
using System.Threading.Tasks;

namespace ProtoDemo02
{
    class Program
    {
        static void Main(string[] args)
        {


            var props = new Props()// the producer is a delegate that returns a new instance of an IActor
                .WithProducer(() => new MyActor())
                // the default dispatcher uses the thread pool and limits throughput to 300 messages per mailbox run
                .WithDispatcher(new ThreadPoolDispatcher { Throughput = 300 })
                // the default mailbox uses unbounded queues
                .WithMailbox(() => UnboundedMailbox.Create(new MyMailboxStatistics()))
                // the default strategy restarts child actors a maximum of 10 times within a 10 second window
                .WithChildSupervisorStrategy(new OneForOneStrategy((who, reason) => SupervisorDirective.Restart, 10, TimeSpan.FromSeconds(10)))
                // middlewares can be chained to intercept incoming and outgoing messages
                // receive middlewares are invoked before the actor receives the message
                // sender middlewares are invoked before the message is sent to the target PID
                .WithReceiveMiddleware(
                next => async c =>
                {
                    Console.WriteLine($"Receive middleware 1 enter {c.Message.GetType()}:{c.Message}");
                    await next(c);
                    Console.WriteLine($"Receive middleware 1 exit");
                },
                next => async c =>
                {
                    Console.WriteLine($"Receive middleware 2 enter {c.Message.GetType()}:{c.Message}");
                    await next(c);
                    Console.WriteLine($"Receive middleware 2 exit");
                })
                .WithSenderMiddleware(
                next => async (c, target, envelope) =>
                {
                    Console.WriteLine($"Sender middleware 1 enter {c.Message.GetType()}:{c.Message}");
                    await next(c, target, envelope);
                    Console.WriteLine($"Sender middleware 1 enter {c.Message.GetType()}:{c.Message}");
                },
                next => async (c, target, envelope) =>
                {
                    Console.WriteLine($"Sender middleware 2 enter {c.Message.GetType()}:{c.Message}");
                    await next(c, target, envelope);
                    Console.WriteLine($"Sender middleware 2 enter {c.Message.GetType()}:{c.Message}");
                })
                // the default spawner constructs the Actor, Context and Process
                .WithSpawner(Props.DefaultSpawner);

            var pid = Actor.Spawn(props);
            pid.Tell(new Hello { Who = "孙菲菲" });
            Console.ReadLine();
        }
    }

    public class MyMailboxStatistics : IMailboxStatistics
    {
        public void MailboxEmpty()
        {
            Console.WriteLine("我的类MyMailboxStatistics.MailboxEmpty");
        }

        public void MailboxStarted()
        {
            Console.WriteLine("我的类MyMailboxStatistics.MailboxStarted");
        }

        public void MessagePosted(object message)
        {
            Console.WriteLine("我的类MyMailboxStatistics.MessagePosted:" + message);
        }

        public void MessageReceived(object message)
        {
            Console.WriteLine("我的类MyMailboxStatistics.MessageReceived:" + message);
        }
    }

    public class MyActor : IActor
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

    public class Hello
    {
        public string Who;
    }
}
