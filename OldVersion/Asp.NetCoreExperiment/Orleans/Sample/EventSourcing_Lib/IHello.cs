
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
        Task<string> Method1();

        Task<string> Method2();

        Task TTT();
    }
    [Serializable]
    public class HelloState
    {
        public DateTime UpdateTime { get; set; }

        public double Amount { get; set; }
        public override string ToString()
        {
            return $"UpdateTime:{UpdateTime} Amount:{Amount}";
        }

        public void Apply(HelloEvent1 @event)
        {
            this.UpdateTime = DateTime.Now;
            this.Amount += 5;
            Console.WriteLine($"事件：{@event}");
        }
        public void Apply(HelloEvent2 @event)
        {
            this.UpdateTime = DateTime.Now;
            this.Amount += 15;
            Console.WriteLine($"事件：{@event}");
        }
    }

    public interface IHelloEvent
    {
        int ID { get; }
    }
    [Serializable]
    public class HelloEvent1 : IHelloEvent
    {
        public int ID { get { return 1; } }
        public Guid Guid { get; set; }
        public string User { get; set; }
        public DateTime CreateTime { get; set; }
        public override string ToString()
        {
            return $"Guid:{Guid.NewGuid().ToString()},User:{User},CreateTime:{CreateTime}";
        }
    }
    [Serializable]
    public class HelloEvent2 : IHelloEvent
    {
        public int ID { get { return 2; } }
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
    public class HelloGrain : JournaledGrain<HelloState, IHelloEvent>, IHelloGrain
    {
        public async Task TTT()
        {
            foreach (var @event in await RetrieveConfirmedEvents(1, 3))
            {
                Console.WriteLine(@event);
            }
        }

        public async Task<string> Method1()
        {
            Console.WriteLine($"Write这是状态State：  {this.State}");
            Console.WriteLine($"Write这是版本Version:{Version}");

            RaiseEvent(new HelloEvent1
            {
                CreateTime = DateTime.Now,
                User = "桂素伟",
                Guid = new Guid()
            });
            await ConfirmEvents();
            return await Task.FromResult($"结果:{State}");
        }
        public async Task<string> Method2()
        {
            Console.WriteLine($"Write这是状态State：  {this.State}");
            Console.WriteLine($"Write这是版本Version:{Version}");

            RaiseEvent(new HelloEvent2
            {
                CreateTime = DateTime.Now,
                User = "桂素伟",
                Guid = new Guid()
            });
            await ConfirmEvents();
            return await Task.FromResult($"结果:{State}");
        }
        protected override void OnStateChanged()
        {
            Console.WriteLine("测试 OnStateChanged");
            base.OnStateChanged();
        }

        protected override void TransitionState(HelloState state, IHelloEvent @event)
        {
            //在这里根据事件更新状态
            Console.WriteLine("测试 TransitionState");
            switch (@event.ID)
            {
                case 1:
                    state.UpdateTime = DateTime.Now;
                    state.Amount += 10;
                    break;
                case 2:
                    state.UpdateTime = DateTime.Now;
                    state.Amount += 100;
                    break;
            }

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
            Console.WriteLine($"NumberOfConsecutiveFailures:{issue.NumberOfConsecutiveFailures}");
            Console.WriteLine($"RetryDelay(s):{issue.RetryDelay.TotalSeconds}");
            Console.WriteLine($"TimeOfFirstFailure:{issue.TimeOfFirstFailure}");
            Console.WriteLine($"TimeStamp:{issue.TimeStamp}");
            base.OnConnectionIssue(issue);
        }
        protected override void OnConnectionIssueResolved(ConnectionIssue issue)
        {
            Console.WriteLine("测试 OnConnectionIssueResolved");
            Console.WriteLine($"NumberOfConsecutiveFailures:{issue.NumberOfConsecutiveFailures}");
            Console.WriteLine($"RetryDelay(s):{issue.RetryDelay.TotalSeconds}");
            Console.WriteLine($"TimeOfFirstFailure:{issue.TimeOfFirstFailure}");
            Console.WriteLine($"TimeStamp:{issue.TimeStamp}");
            base.OnConnectionIssueResolved(issue);
        }

    }

}
