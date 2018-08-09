using Microsoft.Data.Sqlite;
using Proto;
using Proto.Persistence;
using Proto.Persistence.Sqlite;
using Proto.Remote;
using Proto.Serialization.Wire;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceA
{
    class Program
    {
        static void Main(string[] args)
        {
            var wire = new WireSerializer(new[] { typeof(HelloRequest), typeof(HelloResponse) });
            Serialization.RegisterSerializer(wire, true);


            Console.Title = "服务端";              
            var actorid = "this1234";
            var dbfile = @"C:\MyFile\Source\Repos\Asp.NetCoreExperiment\Asp.NetCoreExperiment\ProtoActor\ServiceA\data.sqlite";
            var sqliteProvider = new SqliteProvider(new SqliteConnectionStringBuilder() { DataSource = dbfile });

            var localActor = new LocalActor(sqliteProvider, actorid);
            var props = Actor.FromProducer(()=>localActor);

            var props1 = Actor.FromFunc(ctx =>
            {                
                switch (ctx.Message)
                {
                    case HelloRequest msg:
                        Console.WriteLine(msg.Message);
                        ctx.Respond(new HelloResponse
                        {
                            Message = "回应：我是服务端",
                        });
                        break;
                    default:
                        break;
                }
                return Actor.Done;
            });

            Remote.RegisterKnownKind("hello", props);
            Remote.Start("127.0.0.1", 12000);
            Console.WriteLine("服务端开始……");
            Console.ReadLine();





            Console.ReadLine();
        }

        public class LocalActor : IActor
        {
            private readonly Persistence _persistence;
            public LocalActor(IEventStore eventStore, string actorId)
            {                
                _persistence = Persistence.WithEventSourcing(eventStore, actorId, ApplyEvent);
            }
            private void ApplyEvent(Proto.Persistence.Event @event)
            {
                switch (@event.Data)
                {
                    case HelloRequest msg:
                        

                        break;
                }
            }
            public Task ReceiveAsync(IContext context)
            {
                switch (context.Message)
                {
                    case HelloRequest _:

                        break;
                }
                return Actor.Done;
            }
        }

        public class HelloRequest
        {
            public string Message
            {
                get; set;
            }
        }

        public class HelloResponse
        {
            public string Message
            { get; set; }
        }
    }

}
