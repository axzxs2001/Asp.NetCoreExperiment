using Akka.Actor;
using Akka.Configuration;
using Akka.Event;
using Akka.Routing;
using System;
using System.Linq;
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
            //var system = ActorSystem.Create("mysystem");
            //var router = system.ActorOf(Props.Create<Worker>().WithRouter(new RoundRobinPool(3)), "workers");
            //router.Tell(123);
            //router.Tell(456);
            //router.Tell(789);
            //router.Tell(123);
            //router.Tell(123);
            //router.Tell(456);
            //router.Tell(789);
            //router.Tell(123);
            //router.Tell(123);
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
            //          var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
            ///some-group {
            //  router = broadcast-group
            //  routees.paths = [""/user/a1"",""/user/a2"", ""/user/a3""]
            //}
            //  }");
            //          var system = ActorSystem.Create("mysystem", config);
            //          system.ActorOf<Worker>("a1");
            //          system.ActorOf<Worker>("a2");
            //          system.ActorOf<Worker>("a3");
            //          var router = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "some-group");
            //          router.Tell(111);
            #endregion
            #region broadcast-group   

            //var system = ActorSystem.Create("mysystem");
            //system.ActorOf<Worker>("a1");
            //system.ActorOf<Worker>("a2");
            //system.ActorOf<Worker>("a3");
            //var actors = new[] { "/user/a1", "/user/a2", "/user/a3" };
            //var router = system.ActorOf(Props.Empty.WithRouter(new BroadcastGroup(actors)), "some-group");

            //router.Tell(111);
            #endregion
            #region  random-pool 配置文件
            //            var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
            //  /some-pool {
            //    router = random-pool
            //    nr-of-instances = 2
            //  }
            //}");
            //            var system = ActorSystem.Create("mysystem", config);
            //            var router = system.ActorOf(Props.Create<Worker>().WithRouter(FromConfig.Instance), "some-pool");
            //            router.Tell(111);
            //            router.Tell(222);
            //            router.Tell(333);
            #endregion
            #region random-pool        
            //var system = ActorSystem.Create("mysystem");
            //var router = system.ActorOf(Props.Create<Worker>().WithRouter(new RandomPool(2)), "some-pool");
            //router.Tell(111);
            //router.Tell(222);
            //router.Tell(333);
            #endregion

            #region random-group 配置文件
            //          var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
            ///some-group {
            //  router = random-group
            //  routees.paths = [""/user/w1"", ""/user/w2"", ""/user/w3""]
            //}
            //  }");
            //          var system = ActorSystem.Create("mysystem", config);
            //          system.ActorOf<Worker>("w1");
            //          system.ActorOf<Worker>("w2");
            //          system.ActorOf<Worker>("w3");
            //          var router = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "some-group");
            //          router.Tell(111);
            //          router.Tell(222);
            //          router.Tell(333);
            #endregion
            #region random-group 

            //var system = ActorSystem.Create("mysystem");
            //var workers = new[] { "/user/w1", "/user/w2", "/user/w3" };
            //var router = system.ActorOf(Props.Empty.WithRouter(new RandomGroup(workers)), "workers");
            //system.ActorOf<Worker>("w1");
            //system.ActorOf<Worker>("w2");
            //system.ActorOf<Worker>("w3");

            //router.Tell(111);
            //router.Tell(222);
            //router.Tell(333);
            //router.Tell(111);
            //router.Tell(222);
            //router.Tell(333);
            #endregion

            #region consistent-hashing-poo 配置文件
            //            var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
            //  /some-pool {
            //    router = consistent-hashing-pool
            //    nr-of-instances = 3
            //    virtual-nodes-factor = 6
            //  }
            //}");
            //var system = ActorSystem.Create("mysystem", config);
            //var router = system.ActorOf(Props.Create<Worker1>().WithRouter(FromConfig.Instance), "some-pool");

            //var guid1 = Guid.NewGuid();
            //var originalMsg1 = new SomeMessage { GroupID = guid1 };
            //var msg1 = new ConsistentHashableEnvelope(originalMsg1, originalMsg1.GroupID);

            //var originalMsg2 = new SomeMessage { GroupID = guid1 };
            //var msg2 = new ConsistentHashableEnvelope(originalMsg2, originalMsg2.GroupID);

            //var originalMsg3 = new SomeMessage { GroupID = Guid.NewGuid() };
            //var msg3 = new ConsistentHashableEnvelope(originalMsg3, originalMsg3.GroupID);

            //var originalMsg4 = new SomeMessage { GroupID = Guid.NewGuid() };
            //var msg4 = new ConsistentHashableEnvelope(originalMsg4, originalMsg4.GroupID);


            //router.Tell(msg1);
            //router.Tell(msg2);
            //router.Tell(msg3);
            //router.Tell(msg4);

            #endregion

            #region consistent-hashing-poo
            //var system = ActorSystem.Create("mysystem");
            //var router = system.ActorOf(Props.Create<Worker1>().WithRouter(new ConsistentHashingPool(51)), "some-pool");

            //var guid1 = Guid.NewGuid();
            //var originalMsg1 = new SomeMessage { GroupID = guid1 };
            //var msg1 = new ConsistentHashableEnvelope(originalMsg1, originalMsg1.GroupID);

            //var originalMsg2 = new SomeMessage { GroupID = guid1 };
            //var msg2 = new ConsistentHashableEnvelope(originalMsg2, originalMsg2.GroupID);

            //var originalMsg3 = new SomeMessage { GroupID = Guid.NewGuid() };
            //var msg3 = new ConsistentHashableEnvelope(originalMsg3, originalMsg3.GroupID);

            //var originalMsg4 = new SomeMessage { GroupID = Guid.NewGuid() };
            //var msg4 = new ConsistentHashableEnvelope(originalMsg4, originalMsg4.GroupID);

            //Thread.Sleep(1000);
            //router.Tell(msg1);
            //router.Tell(msg2);
            //router.Tell(msg3);
            //router.Tell(msg4);
            #endregion

            #region consistent-hashing-group 配置文件
            //            var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
            //  /some-group {
            //    router = consistent-hashing-group
            //    routees.paths = [""/user/w1"", ""/user/w2"", ""/user/w3"",""/user/w4"", ""/user/w5"", ""/user/w6"",""/user/w7"", ""/user/w8"", ""/user/w9""]
            //    virtual-nodes-factor = 10
            //  }
            //}");
            //            var system = ActorSystem.Create("mysystem", config);
            //            var router = system.ActorOf(Props.Create<Worker1>().WithRouter(FromConfig.Instance), "some-group");
            //            system.ActorOf<Worker1>("w1");
            //            system.ActorOf<Worker1>("w2");
            //            system.ActorOf<Worker1>("w3");
            //            system.ActorOf<Worker1>("w4");
            //            system.ActorOf<Worker1>("w5");
            //            system.ActorOf<Worker1>("w6");
            //            system.ActorOf<Worker1>("w7");
            //            system.ActorOf<Worker1>("w8");
            //            system.ActorOf<Worker1>("w9");
            //            var guid1 = Guid.NewGuid();
            //            var originalMsg1 = new SomeMessage { GroupID = guid1 };
            //            var msg1 = new ConsistentHashableEnvelope(originalMsg1, originalMsg1.GroupID);

            //            var originalMsg2 = new SomeMessage { GroupID = guid1 };
            //            var msg2 = new ConsistentHashableEnvelope(originalMsg2, originalMsg2.GroupID);

            //            var originalMsg3 = new SomeMessage { GroupID = Guid.NewGuid() };
            //            var msg3 = new ConsistentHashableEnvelope(originalMsg3, originalMsg3.GroupID);

            //            var originalMsg4 = new SomeMessage { GroupID = Guid.NewGuid() };
            //            var msg4 = new ConsistentHashableEnvelope(originalMsg4, originalMsg4.GroupID);


            //            router.Tell(msg1);
            //            router.Tell(msg2);
            //            router.Tell(msg3);
            //            router.Tell(msg4);

            #endregion

            #region consistent-hashing-group
            //var system = ActorSystem.Create("mysystem");
            //var workers = new[] { "/user/w1", "/user/w2", "/user/w3", "/user/w4", "/user/w5", "/user/w6", "/user/w7", "/user/w8", "/user/w9" };
            //var router = system.ActorOf(Props.Empty.WithRouter(new ConsistentHashingGroup(workers)), "some-group");
            //system.ActorOf<Worker1>("w1");
            //system.ActorOf<Worker1>("w2");
            //system.ActorOf<Worker1>("w3");
            //system.ActorOf<Worker1>("w4");
            //system.ActorOf<Worker1>("w5");
            //system.ActorOf<Worker1>("w6");
            //system.ActorOf<Worker1>("w7");
            //system.ActorOf<Worker1>("w8");
            //system.ActorOf<Worker1>("w9");
            //var guid1 = Guid.NewGuid();
            //var originalMsg1 = new SomeMessage { GroupID = guid1 };
            //var msg1 = new ConsistentHashableEnvelope(originalMsg1, originalMsg1.GroupID);

            //var originalMsg2 = new SomeMessage { GroupID = guid1 };
            //var msg2 = new ConsistentHashableEnvelope(originalMsg2, originalMsg2.GroupID);

            //var originalMsg3 = new SomeMessage { GroupID = Guid.NewGuid() };
            //var msg3 = new ConsistentHashableEnvelope(originalMsg3, originalMsg3.GroupID);

            //var originalMsg4 = new SomeMessage { GroupID = Guid.NewGuid() };
            //var msg4 = new ConsistentHashableEnvelope(originalMsg4, originalMsg4.GroupID);


            //router.Tell(msg1);
            //router.Tell(msg2);
            //router.Tell(msg3);
            //router.Tell(msg4);
            #endregion


            #region tail-chopping-pool 配置文件
            //var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
            //  /some-pool {
            //    router = tail-chopping-pool
            //    nr-of-instances = 5
            //    within = 10s
            //    tail-chopping-router.interval = 20ms
            //  }
            //}");

            //var system = ActorSystem.Create("mysystem", config);
            //var router = system.ActorOf(Props.Create<Worker>().WithRouter(FromConfig.Instance), "some-pool");

            //router.Tell(123);


            #endregion
            #region tail-chopping-pool
            //var system = ActorSystem.Create("mysystem");
            //var within = TimeSpan.FromSeconds(10);
            //var interval = TimeSpan.FromMilliseconds(20);
            //var router = system.ActorOf(Props.Create<Worker2>().WithRouter(new TailChoppingPool(3, within, interval)), "some-pool");


            //var result = router.Ask(123).Result;
            //Console.WriteLine($"{router.Path}   返回：{result}");

            #endregion

            #region tail-chopping-group 配置文件
            //var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
            //  /some-group {
            //    router = tail-chopping-group
            //    routees.paths = [""/user/w1"", ""/user/w2"", ""/user/w3""]
            //    within = 10s
            //    tail-chopping-router.interval = 20ms
            //  }
            //}");

            //var system = ActorSystem.Create("mysystem", config);
            //var router = system.ActorOf(Props.Create<Worker2>().WithRouter(FromConfig.Instance), "some-group");
            //system.ActorOf<Worker2>("w1");
            //system.ActorOf<Worker2>("w2");
            //system.ActorOf<Worker2>("w3");

            //var result = router.Ask(123).Result;
            //Console.WriteLine(result);


            #endregion

            #region tail-chopping-group   

            //var system = ActorSystem.Create("mysystem");
            //var workers = new[]{"/user/w1","/user/w2","/user/w3"};
            //var within = TimeSpan.FromSeconds(10);
            //var interval = TimeSpan.FromMilliseconds(20);
            //var router = system.ActorOf(Props.Create<Worker2>().WithRouter(new TailChoppingGroup(workers, within, interval)), "some-group");

            //var result = router.Ask(123).Result;
            //Console.WriteLine(result);

            #endregion

            #region scatter-gather-pool 配置文件  
            //            var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
            //  /some-pool {
            //    router = scatter-gather-pool
            //    nr-of-instances = 5
            //    within = 10s
            //  }
            //}");
            //            var system = ActorSystem.Create("mysystem", config);
            //            var router = system.ActorOf(Props.Create<Worker2>().WithRouter(FromConfig.Instance), "some-pool");
            //            var result = router.Ask(123).Result;
            //            Console.WriteLine($"{router.Path}   返回：{result}");
            #endregion

            #region scatter-gather-pool    
            //var system = ActorSystem.Create("mysystem");
            //var within = TimeSpan.FromSeconds(10);
            //var router = system.ActorOf(Props.Create<Worker2>().WithRouter(new ScatterGatherFirstCompletedPool(5, within)), "some-pool");
            //var result = router.Ask(123).Result;
            //Console.WriteLine($"{router.Path}   返回：{result}");
            #endregion
            #region sscatter-gather-group 配置文件  
            //            var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
            //  /some-group {
            //    router = scatter-gather-group
            //    routees.paths = [""/user/w1"", ""/user/w2"", ""/user/w3""]
            //    within = 10s
            //  }
            //}");
            //            var system = ActorSystem.Create("mysystem", config);
            //            var router = system.ActorOf(Props.Create<Worker2>().WithRouter(FromConfig.Instance), "some-group");
            //            var result = router.Ask(123).Result;
            //            Console.WriteLine($"{router.Path}   返回：{result}");
            #endregion
            #region sscatter-gather-group  
            //var workers = new[] { "/user/w1", "/user/w2", "/user/w3" };
            //var within = TimeSpan.FromSeconds(10);
            //var system = ActorSystem.Create("mysystem");
            //var router = system.ActorOf(Props.Create<Worker2>().WithRouter(new ScatterGatherFirstCompletedGroup(workers, within)), "some-group");
            //system.ActorOf<Worker2>("w1");
            //system.ActorOf<Worker2>("w2");
            //system.ActorOf<Worker2>("w3");
            //var result = router.Ask(123).Result;
            //Console.WriteLine($"{router.Path}   返回：{result}");
            #endregion

            #region SmallestMailbox
            //            var config = ConfigurationFactory.ParseString(@"akka.actor.deployment {
            //  /some-pool {
            //    router = smallest-mailbox-pool
            //    nr-of-instances = 5
            //  }
            //}");
            //            var system = ActorSystem.Create("mysystem", config);
            //            var router = system.ActorOf(Props.Create<Worker2>().WithRouter(FromConfig.Instance), "some-pool");
            //            var result1 = router.Ask(123).Result;
            //            Console.WriteLine($"{router.Path}   返回：{result1}");
            //            var result2 = router.Ask(123).Result;
            //            Console.WriteLine($"{router.Path}   返回：{result2}");
            //            var result3 = router.Ask(123).Result;
            //            Console.WriteLine($"{router.Path}   返回：{result3}");
            //            var result4 = router.Ask(123).Result;
            //            Console.WriteLine($"{router.Path}   返回：{result4}");
            //            var result5 = router.Ask(123).Result;
            //            Console.WriteLine($"{router.Path}   返回：{result5}");
            //            var result6 = router.Ask(123).Result;
            //            Console.WriteLine($"{router.Path}   返回：{result6}");
            #endregion


            #region PoisonPill
            //var actorSystem = ActorSystem.Create("mysystem");

            //actorSystem.ActorOf(Props.Create<Worker3>(), "worker1");
            //actorSystem.ActorOf(Props.Create<Worker3>(), "worker2");
            //actorSystem.ActorOf(Props.Create<Worker3>(), "worker3");


            //var workers = new[] { "/user/worker1", "/user/worker2", "/user/worker3" };
            // var router = actorSystem.ActorOf(Props.Create<Worker3>().WithRouter(new RoundRobinGroup(workers)), "workers");


            //// this sends to individual worker
            //router.Tell("Hello, worker1");
            //router.Tell("Hello, worker2");
            //router.Tell("Hello, worker3");
            //Thread.Sleep(2000);
            //Console.WriteLine("---------------------------------");

            //// this sends to all workers
            // router.Tell(new Broadcast("Hello, workers"));


            #endregion

            #region PoisonPill
            //var actorSystem = ActorSystem.Create("mysystem");
            //actorSystem.ActorOf(Props.Create<Worker3>(), "worker1");
            //actorSystem.ActorOf(Props.Create<Worker3>(), "worker2");
            //actorSystem.ActorOf(Props.Create<Worker3>(), "worker3");

            //var workers = new[] { "/user/worker1", "/user/worker2", "/user/worker3" };
            //var router = actorSystem.ActorOf(Props.Create<Worker3>().WithRouter(new RoundRobinGroup(workers)), "workers");

            //router.Tell("Hello, worker1");
            //router.Tell("Hello, worker2");
            //router.Tell("Hello, worker3");

            //// this sends PoisonPill message to all routees
            //router.Tell(new Broadcast(PoisonPill.Instance));

            //router.Tell("Hello, worker1");
            //router.Tell("Hello, worker2");
            //router.Tell("Hello, worker3");
            #endregion

            #region Router Logic
            var system = ActorSystem.Create("mysystem");
            var routees = Enumerable
    .Range(1, 5)
    .Select(i => new ActorRefRoutee(system.ActorOf<Worker3>("w" + i)))
    .ToArray();

            var router = new Router(new RoundRobinRoutingLogic(), routees);

            for (var i = 0; i < 10; i++)
                router.Route("msg #" + i, ActorRefs.NoSender);

       
            #endregion

            Console.ReadLine();
        }
        public class SomeMessage
        {
            public Guid GroupID { get; set; }
        }

        public class Worker3 : ReceiveActor
        {
            private readonly ILoggingAdapter _log = Context.GetLogger();
            /// <summary>
            /// 无参构造
            /// </summary>
            public Worker3()
            {
                Receive<string>(s =>
                {
                    // Thread.Sleep(2000);
                    _log.Info($"无参构造的Receive:参数{s},Self={Self},Sender={Sender}");
                });
            }

        }
        public class Worker1 : ReceiveActor
        {
            private readonly ILoggingAdapter _log = Context.GetLogger();
            /// <summary>
            /// 无参构造
            /// </summary>
            public Worker1()
            {
                Receive<SomeMessage>(x =>
                {
                    Thread.Sleep(1000);
                    _log.Info($"无参构造的Receive:参数{x.GroupID},Self={Self},Sender={Sender}");
                });
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
                Receive<int>(x =>
                {
                    // Thread.Sleep(2000);
                    _log.Info($"无参构造的Receive:参数{x},Self={Self},Sender={Sender}");
                });
            }

        }

        public class Worker2 : ReceiveActor
        {
            private readonly ILoggingAdapter _log = Context.GetLogger();
            /// <summary>
            /// 无参构造
            /// </summary>
            public Worker2()
            {
                Receive<int>(x =>
                {
                    Thread.Sleep(2000);
                    _log.Info($"无参构造的Receive:参数{x},Self={Self},Sender={Sender}");
                    Sender.Tell("完成！");
                });
            }

        }
    }
}
