
using Akka.Actor;
using Akka.Event;
using System;

namespace Demo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("mysystem");
            //有参
            var myActor = system.ActorOf(DemoActor.Props(42));
            myActor.Tell(8);
            //无参
            var myActor1 = system.ActorOf<DemoActor>("myactor");
            myActor1.Tell(9);

            Console.ReadLine();
        }
    }
    public class DemoActor : ReceiveActor
    {
        /// <summary>
        /// 无参构造
        /// </summary>
        public DemoActor()
        {
            Receive<int>(x =>
            {
                //无参构造的Receive
                Console.WriteLine("无参构造的Receive");
                Console.WriteLine(x);
            });
        }

        private readonly int _magicNumber;

        public DemoActor(int magicNumber)
        {
            _magicNumber = magicNumber;
            Receive<int>(x =>
            {
                //有参构造的Receive  
                Console.WriteLine("有参构造的Receive");
                Console.WriteLine(x);
                if (x < 20)
                {
                    Sender.Tell(x + _magicNumber, Self);
                }
            });
        }

        public static Props Props(int magicNumber)
        {
            return Akka.Actor.Props.Create(() => new DemoActor(magicNumber));
        }
    }

    public class DemoMessagesActor : ReceiveActor
    {
        public class Greeting
        {
            public Greeting(string from)
            {
                From = from;
            }
            public string From { get; }
        }

        public class Goodbye
        {
            public static Goodbye Instance = new Goodbye();

            private Goodbye() { }
        }

        private ILoggingAdapter log = Context.GetLogger();

        public DemoMessagesActor()
        {
            Receive<Greeting>(greeting =>
            {
                Sender.Tell($"I was greeted by {greeting.From}", Self);
            });

            Receive<Goodbye>(_ =>
            {
                log.Info("Someone said goodbye to me.");
            });
        }
    }

}
