using Microsoft.Data.Sqlite;
using Proto;
using Proto.Persistence;
using Proto.Persistence.Sqlite;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProtoPersistenceDemo
{
    class MyEventStore : IEventStore
    {
        Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
        public Task DeleteEventsAsync(string actorName, long inclusiveToIndex)
        {
            if (dic.ContainsKey(actorName))
            {
                dic.Remove(actorName);
            }
            return Task.Run(() => { });
        }

        public Task<long> GetEventsAsync(string actorName, long indexStart, long indexEnd, Action<object> callback)
        {
            if (dic.ContainsKey(actorName))
            {
                callback(dic[actorName].@event);
            }
            return Task.Run(() => { return indexStart; });
        }

        public Task<long> PersistEventAsync(string actorName, long index, object @event)
        {
            if (!dic.ContainsKey(actorName))
            {
                dic.Add(actorName, new { index, @event });
            }
            return Task.Run(() => { return index; });
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var actorid = "this1234";
            var a = new MyEventStore();
            var c = new Counter(new MyEventStore(), actorid);
            var props = Actor.FromProducer(() => c);
            var pid = Actor.Spawn(props);
            for (int i = 0; i < 1; i++)
            {
                var no = int.Parse(Console.ReadLine());
                pid.Tell(new Added { Amount = no });
            }
            var props2 = Actor.FromProducer(() => c);
            var pid2 = Actor.Spawn(props2);
            for (int i = 0; i < 2; i++)
            {
                var no = int.Parse(Console.ReadLine());
                pid2.Tell(new Added { Amount = no });
            }
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
