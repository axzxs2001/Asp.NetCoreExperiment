using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 状态模式
    * 当一个对象的内在状态改变时允许改变其行为，这个对象看起来像是改变了其类。
    ****************************************************************************/


    /// <summary>
    /// 内容状态
    /// </summary>
    public class StateContext
    {
        State _state;
        public StateContext(State state)
        {
            _state = state;
        }
        public State State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                Console.WriteLine($"状态：{_state.GetType().Name}");
            }
        }

        public void Request()
        {
            State.Handle(this);
        }
    }
    /// <summary>
    /// 抽象状态类
    /// </summary>
    public abstract class State
    {
        public abstract void Handle(StateContext context);
    }
    /// <summary>
    /// 状态A
    /// </summary>
    public class StateA : State
    {
        public override void Handle(StateContext context)
        {
            context.State = new StateB();
        }
    }
    /// <summary>
    /// 状态B
    /// </summary>
    public class StateB : State
    {
        public override void Handle(StateContext context)
        {
            context.State = new StateA();
        }
    }

}
