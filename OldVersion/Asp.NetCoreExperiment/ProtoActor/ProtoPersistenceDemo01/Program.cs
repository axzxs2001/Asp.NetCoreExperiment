using Microsoft.Data.Sqlite;
using Proto;
using Proto.Persistence;
using Proto.Persistence.Sqlite;
using System;
using System.Threading.Tasks;

namespace ProtoPersistenceDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            //用sqlite持久化后
            var actorid = "this1234";
            var dbfile = @"C:\MyFile\Source\Repos\Asp.NetCoreExperiment\Asp.NetCoreExperiment\ProtoActor\ProtoPersistenceDemo01\data.sqlite";
            var sqliteProvider = new SqliteProvider(new SqliteConnectionStringBuilder() { DataSource= dbfile });            
            var counter = new Counter(sqliteProvider, actorid);
            var props = Actor.FromProducer(() => counter);
            var pid = Actor.Spawn(props);
            for (int i = 0; i < 2; i++)
            {
                var no = int.Parse(Console.ReadLine());
                pid.Tell(new Added { Amount = no });               
            } 

            Console.ReadLine();
            //完成处理后清理持久化的操作          
            sqliteProvider.DeleteEventsAsync(actorid, 10).Wait();
        }
    }
    public class Added
    {
        public int Amount { get; set; }
    }
    public class Counter : IActor
    {
        private int _value = 0;
        private readonly Persistence _persistence;

        public Counter(IEventStore eventStore, string actorId)
        {
            _persistence = Persistence.WithEventSourcing(eventStore, actorId, ApplyEvent);
        }
        private void ApplyEvent(Proto.Persistence.Event @event)
        {
            switch (@event.Data)
            {
                case Added msg:
                    _value = _value + msg.Amount;
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
                case Added msg:
                    if (msg.Amount > 0)
                    {
                        await _persistence.PersistEventAsync(new Added { Amount = msg.Amount });
                    }
                    break;
            }
        }
    }
}
