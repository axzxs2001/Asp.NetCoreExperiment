using Akka.Actor;
using Akka.Event;
using Akka.Persistence;
using System;
using System.Collections.Immutable;

namespace Demo06
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("PersistAsync");
            var persistentActor = system.ActorOf<PersistentActor>();

            var handler = system.ActorOf(Props.Create<MyEventHandler>());
            system.EventStream.Subscribe(handler, typeof(Evt));

            persistentActor.Tell(new Cmd("cmd data 1"));
            persistentActor.Tell(new Cmd("cmd data 2"));
            persistentActor.Tell("snap");
            persistentActor.Tell(new Cmd("cmd data 3"));
            persistentActor.Tell(new Cmd("cmd data 4"));
            persistentActor.Tell(new Cmd("cmd data 5"));
            
            persistentActor.Tell("print");
            Console.ReadLine();
        }
    }
    public class MyEventHandler : UntypedActor
    {
        ILoggingAdapter log = Context.GetLogger();
        protected override void OnReceive(object message)
        {
            if (message is Evt)
            {
                log.Info($"接收：{(message as Evt).Data}");
            }
        }
    }
    public class Cmd
    {
        public Cmd(string data)
        {
            Data = data;
        }

        public string Data { get; }
    }

    public class Evt
    {
        public Evt(string data)
        {
            Data = data;
        }

        public string Data { get; }
    }

    public class ExampleState
    {
        private readonly ImmutableList<string> _events;

        public ExampleState(ImmutableList<string> events)
        {
            _events = events;
        }

        public ExampleState() : this(ImmutableList.Create<string>())
        {
        }

        public ExampleState Updated(Evt evt)
        {
            return new ExampleState(_events.Add(evt.Data));
        }

        public int Size => _events.Count;

        public override string ToString()
        {
            return string.Join(", ", _events.Reverse());
        }
    }

    public class PersistentActor : UntypedPersistentActor
    {
        private ExampleState _state = new ExampleState();

        private void UpdateState(Evt evt)
        {
            _state = _state.Updated(evt);
        }

        private int NumEvents => _state.Size;

        protected override void OnRecover(object message)
        {
            switch (message)
            {
                case Evt evt:
                    UpdateState(evt);
                    break;
                case SnapshotOffer snapshot when snapshot.Snapshot is ExampleState:
                    _state = (ExampleState)snapshot.Snapshot;
                    break;
                case RecoveryCompleted recoveryCompleted:
                    Console.WriteLine("Recovery Completed");
                    break;
            }
        }

        protected override void OnCommand(object message)
        {
           
            switch (message)
            {
                case Cmd cmd:
                    if("cmd data 3"==cmd.Data)
                    {
                        return;
                    }
                    Persist(new Evt($"{cmd.Data}-{NumEvents}"), UpdateState);
                    Persist(new Evt($"发布-{cmd.Data}-{NumEvents + 1}"), evt =>
                    {
                        UpdateState(evt);
                        Context.System.EventStream.Publish(evt);
                    });
                    break;
                case "snap":
                    SaveSnapshot(_state);
                    break;
                case "print":
                    Console.WriteLine(_state);
                    break;
            }
        }

        public override string PersistenceId { get; } = "sample-id-1";

       // public override Recovery Recovery => new Recovery(fromSnapshot:SnapshotSelectionCriteria.None);
       // public override Recovery Recovery => Recovery.None;
    }
}
