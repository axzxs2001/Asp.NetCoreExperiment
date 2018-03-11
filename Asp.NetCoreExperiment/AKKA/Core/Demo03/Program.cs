using Akka.Actor;
using Akka.Event;
using System;

namespace Demo03
{
    class Program
    {
        static void Main(string[] args)
        {
            #region InBox
            //InboxDemo();
            #endregion





            Console.ReadLine();
        }
        static void InboxDemo()
        {
            var system = ActorSystem.Create("mysystem");
            var target = system.ActorOf(Props.Create<Worker>());
            var inbox = Inbox.Create(system);

            inbox.Send(target, "hello");

            try
            {
                var result = inbox.Receive(TimeSpan.FromSeconds(1)).Equals("world");
                Console.WriteLine($"Inbox.Receive={result}");
             
            }
            catch (TimeoutException tExc)
            {
                Console.WriteLine($"TimeoutException:{tExc.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"总异常：{e.Message}");
            }
        }
    }

    public class Worker : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        /// <summary>
        /// 无参构造
        /// </summary>
        public Worker()
        {
            Receive<string>(str =>
            {
                // Thread.Sleep(2000);
                _log.Info($"无参构造的Receive:参数{str},Self={Self},Sender={Sender}");
                Sender.Tell("world");
            });
        }

    }
}
