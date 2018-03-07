using Akka.Actor;
using Akka.Configuration;
using Akka.Event;
using Akka.Routing;
using System;
using System.Threading;

namespace Demo02
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("mysystem");
            #region router
            //var system = ActorSystem.Create("mysystem");
            ////var props = Props.Create<Worker>().WithRouter(new RoundRobinPool(5));
            //var props = new RoundRobinPool(2).Props(Props.Create<Worker>());
            //var actor = system.ActorOf(props, "worker");
            //actor.Tell(123);
            //actor.Tell(456);
            //actor.Tell(789);
            #endregion

            #region 配置文件
            //            var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
            //  /workers {
            //    router = round-robin-pool
            //    nr-of-instances = 5
            //  }
            //}");
            //Console.WriteLine(@"akka.actor.deployment {
            ///workers {
            //  router = round-robin-group
            //  routees.paths = [""/user/workers/w1"", ""/user/workers/w2"", ""/user/workers/w3""]
            //}
            //  }");
            //var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
            ///workers {
            //  router = round-robin-group
            //  routees.paths = [""/user/workers/w1"", ""/user/workers/w2"", ""/user/workers/w3""]
            //}
            //  }");
            //var system = ActorSystem.Create("mysystem", config);
            //var props1 = Props.Create<Worker>().WithRouter(FromConfig.Instance);
            //var actor1 = system.ActorOf(props1, "workers");
            //Thread.Sleep(3000);
            //actor1.Tell(123);
            #endregion

            #region 
            var router = system.ActorOf(Props.Create<Worker>().WithRouter(new RoundRobinPool(2)), "workers");
            router.Tell(123);
            router.Tell(456);
            router.Tell(789);
            #endregion

            #region RoundRobinGroup
            //system.ActorOf<Worker>("workers1");
            //system.ActorOf<Worker>("workers2");
            //system.ActorOf<Worker>("workers3");
            //var workers = new[] { "/user/workers1", "/user/workers2", "/user/workers3" };
            //var props = Props.Create<Worker>().WithRouter(new RoundRobinGroup(workers));

            //var router = system.ActorOf(props);
            //Console.WriteLine(router.Path);

            //router.Tell(123);
            //router.Tell(456);
            //router.Tell(789);
            #endregion


            Console.ReadLine();
        }

        public class Worker : ReceiveActor
        {
            private readonly ILoggingAdapter _log = Context.GetLogger();
            /// <summary>
            /// 无参构造
            /// </summary>
            public Worker()
            {
                Receive<int>(x =>
                {
                    _log.Info($"无参构造的Receive:参数{x},Self={Self},Sender={Sender}");   
                });
            }

        }
    }
}
