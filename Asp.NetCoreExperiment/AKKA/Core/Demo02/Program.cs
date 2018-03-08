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

            #region RoundRobinPool
            // var system = ActorSystem.Create("mysystem");
            //var router = system.ActorOf(Props.Create<Worker>().WithRouter(new RoundRobinPool(2)), "workers");
            //router.Tell(123);
            //router.Tell(456);
            //router.Tell(789);
            #endregion

            #region RoundRobinGroup
            // var system = ActorSystem.Create("mysystem");
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

            #region  broadcast-pool 配置文件
            //            var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
            //  /some-pool {
            //    router = broadcast-pool
            //    nr-of-instances = 5
            //  }
            //");
            //            var system = ActorSystem.Create("mysystem", config);
            //            var router = system.ActorOf(Props.Create<Worker>().WithRouter(FromConfig.Instance), "some-pool");
            //            router.Tell(111);

            #endregion
            #region  broadcast-pool
            //var system = ActorSystem.Create("mysystem");
            //var router = system.ActorOf(Props.Create<Worker>().WithRouter(new BroadcastPool(5)), "some-pool");
            //router.Tell(111);

            #endregion 
            #region broadcast-group 配置文件  
            var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
  /some-group {
    router = broadcast-group
    routees.paths = [""/user/a1"",""/user/a2"", ""/user/a3""]
  }
    }");
            var system = ActorSystem.Create("mysystem", config);
            system.ActorOf<Worker>("a1");
            system.ActorOf<Worker>("a2");
            system.ActorOf<Worker>("a3");
            var router = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "some-group");
            router.Tell(111);
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
