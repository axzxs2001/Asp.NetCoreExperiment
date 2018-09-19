
using Orleans;
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
        Task<string> Write();
    }
    [Serializable]
    public class HelloState
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public double Amount { get; set; }
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
        public async Task<string> Write()
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
            return await Task.FromResult("结果");

            //await RetrieveConfirmedEvents(0, Version);
            //EnableStatsCollection();
            //DisableStatsCollection();
            //var state = GetStats();
            //var s = TentativeState;

        }
        protected override void OnStateChanged()
        {
            Console.WriteLine("测试 OnStateChanged");
            base.OnStateChanged();
        }

        protected override void TransitionState(HelloState state, HelloEvent @event)
        {
            Console.WriteLine("测试 TransitionState");
            base.TransitionState(state, @event);
        }
        protected override void OnTentativeStateChanged()
        {
            Console.WriteLine("测试 OnTentativeStateChanged");
            base.OnTentativeStateChanged();
        }
        public override void Participate(IGrainLifecycle lifecycle)
        {
            Console.WriteLine("测试 Participate");
            base.Participate(lifecycle);
        }

        protected override void OnConnectionIssue(ConnectionIssue issue)
        {
            Console.WriteLine("测试 OnConnectionIssue");
            base.OnConnectionIssue(issue);
        }
        protected override void OnConnectionIssueResolved(ConnectionIssue issue)
        {
            Console.WriteLine("测试 OnConnectionIssueResolved");
            base.OnConnectionIssueResolved(issue);
        }
    }

}
