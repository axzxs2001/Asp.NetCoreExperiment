using Microsoft.Data.Sqlite;
using Proto;
using Proto.Persistence;
using Proto.Persistence.Sqlite;
using Proto.Remote;
using Proto.Serialization.Wire;
using ServiceEntity;
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
            var props = Actor.FromProducer(() => localActor);

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
            private  void ApplyEvent(Proto.Persistence.Event @event)
            {
                switch (@event.Data)
                {
                    case HelloRequest req:
                         Handle(req);
                        //处理成功后删除保存的状态
                         _persistence.DeleteEventsAsync(2);
                        break;
                }
            }
            public async Task ReceiveAsync(IContext context)
            {
                switch (context.Message)
                {
                    case Started _:
                        await _persistence.RecoverStateAsync();
                        break;
                    case HelloRequest req:
                        //首先把状态保存起来
                        await _persistence.PersistEventAsync(new HelloRequest { Amount = req.Amount });
                         Handle(req);
                        //处理成功后删除保存的状态
                        if (req.Amount != 5)
                        {
                            await _persistence.DeleteEventsAsync(100);
                        }
                        context.Respond(new HelloResponse
                        {
                            Message = $"回应：我是服务端,Time={DateTime.Now}",
                        });
                        break;
                }
            }

             void Handle(HelloRequest helloRequest)
            {
                Console.WriteLine($"处理：HelloRequest: Message={helloRequest.Message},Amount={helloRequest.Amount}");
           
            }
        }


    }

}
