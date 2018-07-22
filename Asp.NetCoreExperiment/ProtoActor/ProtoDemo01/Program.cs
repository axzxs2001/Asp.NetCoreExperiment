using System;
using System.Threading.Tasks;
using Proto;
using Proto.Router;

namespace ProtoDemo01
{
    //actor 的一个明确特征是他们能够以线程安全的方式改变他们的状态，并且他们根据他们收到的消息内容来做这件事。
    /* message definition */
    public class MyMessage1
    {
        public MyMessage1(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
    public class MyMessage2
    {
        public MyMessage2(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    public class MyMessage { }


    /* actor definition */
    public class MyActor : IActor
    {
        string lastActorName;

        public Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case MyMessage1 msg:
                    Console.WriteLine("MyMessage1:" + lastActorName);
                    lastActorName = msg.Name;
                    break;
                case MyMessage2 msg:
                    Console.WriteLine("MyMessage2:" + lastActorName);
                    lastActorName = msg.Name;
                    break;
                case MyMessage msg:
                    Console.WriteLine("MyMessage:" + lastActorName);
                    break;
            }
            return Actor.Done;
        }

    }

    public class Program
    {
        public static void Main()
        {
            var myActor = Actor.Spawn(Actor.FromProducer(() => new MyActor()));
            myActor.Tell(new MyMessage1("MyMessage1 Value"));
            myActor.Tell(new MyMessage2("MyMessage2 Value"));
            myActor.Tell(new MyMessage());
            Console.ReadLine();
        }
    }
}