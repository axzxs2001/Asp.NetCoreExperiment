using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 中介者模式
    * 用一个中介对象来封装一系列的对象交互。中介者使各对象不需要电焊工地相互引用，从而使其耦合松散，而且可以独立地改变它们之间的交互。
    ****************************************************************************/
    /// <summary>
    /// 中介者类
    /// </summary>
    public abstract  class Mediator
    {
        public abstract void Send(string message, Colleague colleague);
    }
    /// <summary>
    /// 中介使用对象类
    /// </summary>
    public abstract class Colleague
    {
        protected Mediator _mediator;
        public Colleague(Mediator mediator)
        {
            _mediator = mediator;
        }

    }
    public class ConcreteColleague1:Colleague
    {
        public ConcreteColleague1(Mediator mediator):base(mediator)
        { }
        public void Send(string message)
        {
            Console.WriteLine($"ConcreteColleague1.Send(message:{message})");
            _mediator.Send(message,this);
        }
        public void Notify(string message)
        {
            Console.WriteLine($"ConcreteColleague1.Notify(message:{message})");
        }
    }
    public class ConcreteColleague2 : Colleague
    {
        public ConcreteColleague2(Mediator mediator) : base(mediator)
        { }
        public void Send(string message)
        {
            Console.WriteLine($"ConcreteColleague2.Send(message:{message})");
            _mediator.Send(message, this);
        }
        public void Notify(string message)
        {
            Console.WriteLine($"ConcreteColleague2.Notify(message:{message})");
        }
    }
    /// <summary>
    /// 具体中介类
    /// </summary>
    public class ConcreteMediator:Mediator
    {
        ConcreteColleague1 _colleague1;
        ConcreteColleague2 _colleague2;
        public ConcreteColleague1 Colleague1
        {
            set
            {
                _colleague1 = value;
            }
        }
        public ConcreteColleague2 Colleague2
        {
            set
            {
                _colleague2 = value;
            }
        }
        public override void Send(string message, Colleague colleague)
        {
           if(_colleague1==colleague)
            {
                _colleague2.Notify(message);
            }
           else
            {
                _colleague1.Notify(message);
            }
        }
    }
}
