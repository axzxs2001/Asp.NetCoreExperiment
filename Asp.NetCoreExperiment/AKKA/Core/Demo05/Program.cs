using Akka.Actor;
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
            var persistentActor = system.ActorOf<MyPersistentActor>();

            // usage
            var result1=persistentActor.Ask("a").Result;
            var result2 = persistentActor.Ask("b").Result;

            Console.WriteLine(result1);

            Console.WriteLine(result2);

            // possible order of received messages:
            // a
            // b
            // evt-a-1
            // evt-a-2
            // evt-b-1
            // evt-b-2
        }
    }

    public class MyPersistentActor : UntypedPersistentActor
    {
        public override string PersistenceId => "my-stable-persistence-id";

        protected override void OnRecover(object message)
        {
            if (message is Akka.Persistence.RecoveryCompleted cm)
            {
               
                Console.WriteLine($"OnRecover.message={message}");
            }
            // handle recovery here
        }

        protected override void OnCommand(object message)
        {
            if (message is string c)
            {
                Sender.Tell($"htis is a {c}");
                Persist($"evt-{c}-1", e => Sender.Tell(e));
                Persist($"evt-{c}-2", e => Sender.Tell(e));
                DeferAsync($"evt-{c}-3", e => Sender.Tell(e));
            }
        }
    }

 
}
