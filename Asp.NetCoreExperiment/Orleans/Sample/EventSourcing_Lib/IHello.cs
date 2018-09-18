
using Orleans;
using Orleans.Concurrency;
using Orleans.EventSourcing;
using Orleans.LogConsistency;
using Orleans.Providers;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace EventSourcing_Lib
{
    public interface IHelloGrain : IGrainWithGuidKey
    {
        Task Write();
    }
    [Serializable]
    public class HelloState
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"ID:{ID},Name:{Name}";
        }

        public void Apply(HelloEvent @event)
        {
            Console.WriteLine($"事件：{@event}");
        }
    }
    [Serializable]
    public class HelloEvent
    {
        public Guid Guid { get; set; }
        public string User { get; set; }
        public DateTime CreateTime { get; set; }

        public override string ToString()
        {
            return $"Guid:{Guid.NewGuid().ToString()},User:{User},CreateTime:{CreateTime}";
        }

    }

    [LogConsistencyProvider(ProviderName = "LogStorage")]
    [StorageProvider(ProviderName = "OrleansStorage")]
    public class HelloGrain : JournaledGrain<HelloState, HelloEvent>, IHelloGrain
    {
        public async Task Write()
        {
            Console.WriteLine($"State  {this.State}");
            Console.WriteLine($"Version:{Version}");

            RaiseEvent(new HelloEvent
            {
                CreateTime = DateTime.Now,
                User = "桂素伟",
                Guid = new Guid()
            });
            await ConfirmEvents();
            await Task.CompletedTask;

            //await RetrieveConfirmedEvents(0, Version);
            //EnableStatsCollection();
            //DisableStatsCollection();
            //var state = GetStats();
            //var s = TentativeState;

        }
        protected override void OnStateChanged()
        {
            Console.WriteLine("OnStateChanged");
            base.OnStateChanged();
        }

        protected override void TransitionState(HelloState state, HelloEvent @event)
        {
            Console.WriteLine("TransitionState");
            base.TransitionState(state, @event);
        }
        protected override void OnTentativeStateChanged()
        {
            Console.WriteLine("OnTentativeStateChanged");
            base.OnTentativeStateChanged();
        }
        public override void Participate(IGrainLifecycle lifecycle)
        {
            Console.WriteLine("Participate");
            base.Participate(lifecycle);
        }

        protected override void OnConnectionIssue(ConnectionIssue issue)
        {
            Console.WriteLine("OnConnectionIssue");
            base.OnConnectionIssue(issue);
        }
        protected override void OnConnectionIssueResolved(ConnectionIssue issue)
        {
            Console.WriteLine("OnConnectionIssueResolved");
            base.OnConnectionIssueResolved(issue);
        }
    }

}
