
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
    public class HelloEvent
    {
        public Guid Guid { get; set; }
        public string User { get; set; }
        public DateTime CreateTime { get; set; }

        public override string ToString()
        {
            return $"Guid:{Guid.ToString()},User:{User},CreateTime:{CreateTime}";
        }

    }

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

            await RetrieveConfirmedEvents(0, Version);

            EnableStatsCollection();
            DisableStatsCollection();
            var state = GetStats();

            var s = TentativeState;

            await Task.CompletedTask;
        }
        protected override void OnStateChanged()
        {
            base.OnStateChanged();
        }

        protected override void TransitionState(HelloState state, HelloEvent @event)
        {
            base.TransitionState(state, @event);
        }
        protected override void OnTentativeStateChanged()
        {
            base.OnTentativeStateChanged();
        }
        public override void Participate(IGrainLifecycle lifecycle)
        {
         
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
