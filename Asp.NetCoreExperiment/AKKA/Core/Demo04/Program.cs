using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Akka.Event;
using Autofac;
using System;
using System.Collections.Immutable;

namespace Demo04
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create and build your container
            var builder = new Autofac.ContainerBuilder();
            //builder.RegisterType<WorkerService>().As<IWorkerService>();
            builder.RegisterType<Worker>();
            var container = builder.Build();

            // Create the ActorSystem and Dependency Resolver
            var system = ActorSystem.Create("MySystem");
            var propsResolver = new AutoFacDependencyResolver(container, system);

            var worker1Ref = system.ActorOf(system.DI().Props<Worker>(), "Worker1");
            worker1Ref.Tell("参数");

            Console.ReadLine();

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
                _log.Info($"Receive.str={str},Self={Self},Sender={Sender}");
            });
        }

    }
    //internal class WorkerService: IWorkerService
    //{
    //}

    //internal interface IWorkerService
    //{
    //}
}
