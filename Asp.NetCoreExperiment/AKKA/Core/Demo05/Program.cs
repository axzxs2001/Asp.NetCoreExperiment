using Akka.Actor;
using Akka.Event;
using Akka.Persistence;
using System;

namespace Demo05
{
    class Program
    {
        static void Main(string[] args)
        {
            MainApp();
            Console.ReadLine();
        }

        public static void MainApp()
        {
            var system = ActorSystem.Create("PersistAsync");
            var persistentActor = system.ActorOf<MyPersistentActor>("persistentActor");



            // usage
            //var result1 = persistentActor.Ask("a").Result;
            //Console.WriteLine(result1);
            //var result2 = persistentActor.Ask("b").Result;
            //Console.WriteLine(result2);

            persistentActor.Tell("a");
         
            persistentActor.Tell("b");

        }
    }
    public class MyEventHandler : UntypedActor
    {
        ILoggingAdapter log = Context.GetLogger();
        protected override void OnReceive(object message)
        {
            log.Info(message.ToString());
        }
    }
    public class MyPersistentActor : UntypedPersistentActor
    {
        public override string PersistenceId => "my-stable-persistence-id";

        protected override void OnRecover(object message)
        {
            if (message is RecoveryCompleted cm)
            {
                Console.WriteLine($"OnRecover.message={message}");
            }          
        }

       
        protected override void OnCommand(object message)
        {
            //if (message is string c)
            //{
            //    Console.WriteLine(c);
            //    //PersistAsync($"evt-{c}-1", e => Console.WriteLine(e));
            //    //PersistAsync($"evt-{c}-2", e => Console.WriteLine(e));
            //    //PersistAsync($"evt-{c}-3", e => Console.WriteLine(e));
            //    Persist($"evt-{c}-1", e => Console.WriteLine(e));
            //    Persist($"evt-{c}-2", e => Console.WriteLine(e));
            //    Persist($"evt-{c}-3", e => Console.WriteLine(e));
            //    DeferAsync($"evt-{c}-4", e => Console.WriteLine(e));
            //}

            //if (message is string c)
            //{              
            //    Console.WriteLine(c);
            //    Persist($"{message}-1-outer", outer1 =>
            //    {
            //        //Sender.Tell(outer1, Self);
            //        Console.WriteLine(outer1);
            //        Persist($"{c}-1-inner", inner1 => Console.WriteLine(inner1));
            //    });

            //    Persist($"{message}-2-outer", outer2 =>
            //    {
            //        Console.WriteLine(outer2);
            //        Persist($"{c}-2-inner", inner2 => Console.WriteLine(inner2));
            //    });
            //}


            if (message is string c)
            {
                Console.WriteLine(c);
                Persist($"{message}-1-outer", outer1 =>
                {                    
                    Console.WriteLine(outer1);
                    PersistAsync($"{c}-1-inner", inner1 => Console.WriteLine(inner1));
                });

                Persist($"{message}-2-outer", outer2 =>
                {
                    Console.WriteLine(outer2);
                    PersistAsync($"{c}-2-inner", inner2 => Console.WriteLine(inner2));
                });
            }
        }
    }


}
