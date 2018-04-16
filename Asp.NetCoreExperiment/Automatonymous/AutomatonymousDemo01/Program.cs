using Automatonymous;
using System;

namespace AutomatonymousDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var relationship = new Relationship();
            var machine = new RelationshipStateMachine();

            machine.RaiseEvent(relationship, machine.Hello);

            var person = new Person { Name = "Joe" };

            machine.RaiseEvent(relationship, machine.Introduce, person);
            Console.ReadLine();
        }
    }


    class Relationship
    {
        public State CurrentState { get; set; }
        public string Name { get; set; }
    }
    class RelationshipStateMachine :
    AutomatonymousStateMachine<Relationship>
    {
        public RelationshipStateMachine()
        {
            // Explicit definition of the events.
            Event(() => Hello);
            Event(() => PissOff);
            Event(() => Introduce);

            // Explicit definition of the states.
            State(() => Friend);
            State(() => Enemy);

            Initially(
                When(Hello)
                    .TransitionTo(Friend),
                When(PissOff)
                    .TransitionTo(Enemy),
                When(Introduce)
                    .Then(ctx => ctx.Instance.Name = ctx.Data.Name)
                    .TransitionTo(Friend)
            );
        }

        public State Friend { get; private set; }
        public State Enemy { get; private set; }

        public Event Hello { get; private set; }
        public Event PissOff { get; private set; }
        public Event<Person> Introduce { get; private set; }
    }

    class Person
    {
        public string Name { get; set; }
    }

}
