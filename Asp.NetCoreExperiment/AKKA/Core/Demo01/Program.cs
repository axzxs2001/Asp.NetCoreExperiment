
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

            #region  DemoActor
            ////有参
            //var demoActor = system.ActorOf(DemoActor.Props(42));
            //demoActor.Tell(8);
            ////无参
            //var demoActor1 = system.ActorOf<DemoActor>("myactor");
            //demoActor1.Tell(9);
            #endregion
            //Console.WriteLine("================================");
            #region DemoMessagesActor
           // var demoMessagesActor = system.ActorOf(DemoMessagesActor.Props());
            //demoMessagesActor.Tell(DemoMessagesActor.Goodbye.Instance);
            //var result=demoMessagesActor.Ask(new DemoMessagesActor.Greeting("greeting参数")).Result;
            //Console.WriteLine(result);
            #endregion
            //Console.WriteLine("================================");
            #region FirstActor
            var firstActor = system.ActorOf<FirstActor>("firstActor");
            firstActor.Tell("123");
            Console.WriteLine("================================");
            firstActor.Tell(456);
            #endregion
            Console.ReadLine();
        }
    }
    public class FirstActor : ReceiveActor
    {
        IActorRef secondActor = Context.ActorOf<SecondActor>("secondActor");
        public FirstActor()
        {
            Receive<int>(x =>
            {
                //无参构造的Receive
                Console.WriteLine($"FirstActor.Receive:参数{x}");
                secondActor.Tell("字符串");
            });
        }
        public class SecondActor : ReceiveActor
        {
            public SecondActor()
            {
                Receive<string>(s =>
                {
                //无参构造的Receive
                Console.WriteLine($"SecondActor.Receive:参数{s}");
                });
            }
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
          
            Console.WriteLine("SupervisorStrategy");
            return base.SupervisorStrategy();
            
        }
        protected override void Unhandled(object message)
        {
            base.Unhandled(message);
            Console.WriteLine("Unhandled方法：" + message);
        }

        protected override void PreStart()
        {          
            Console.WriteLine("FirstActor.PreStart");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine("FirstActor.PreRestart");
            foreach (IActorRef each in Context.GetChildren())
            {
                Context.Unwatch(each);
                Context.Stop(each);
            }
            PostStop();
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine("FirstActor.PostRestart");
            PreStart();
        }

        protected override void PostStop()
        {
            Console.WriteLine("FirstActor.PostStop");
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
                Console.WriteLine($"无参构造的Receive:参数{x}");

            });
        }

        private readonly int _magicNumber;

        public DemoActor(int magicNumber)
        {
            _magicNumber = magicNumber;
            Receive<int>(x =>
            {
                //有参构造的Receive  
                Console.WriteLine($"有参构造的Receive:参数{x}");
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
                Console.WriteLine($"传入参数Greeting：{greeting.From}");
                Sender.Tell($"返回值", Self);
            });

            Receive<Goodbye>(_ =>
            {
                log.Info("Someone said goodbye to me.");
            });
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new DemoMessagesActor());
        }
    }

}
