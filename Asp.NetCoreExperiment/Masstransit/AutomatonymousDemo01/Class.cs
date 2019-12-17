using Automatonymous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatonymousDemo01
{
    public class OrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        //public string CurrentState { get; set; }
        public int CurrentState { get; set; }
    }

    //public class OrderStateMachine : MassTransitStateMachine<OrderState>
    //{
    //    public State Submitted { get; private set; }
    //    public State Accepted { get; private set; }
    //    public OrderStateMachine()
    //    {
    //        InstanceState(x => x.CurrentState, Submitted, Accepted);
    //        //InstanceState(x => x.CurrentState);
    //    }
    //}

    public interface SubmitOrder
    {
        Guid OrderId { get; }
    }

    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            Event(() => SubmitOrder, x => x.CorrelateById(context => context.Message.OrderId));
        }

        public Event<SubmitOrder> SubmitOrder { get; private set; }
    }
}
