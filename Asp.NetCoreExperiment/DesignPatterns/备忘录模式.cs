using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 备忘录模式
    * 在不破坏封闭性的前提下,捕获一个对象的内部状态,并在该对象之外保存这个状态.这样以后就可将该对象恢复到原先保存的状态
    ****************************************************************************/
    /// <summary>
    /// 备忘录类，备访 state这个属性
    /// </summary>
    public class Memento
    {
        string _state;
        public Memento(string state)
        {
            _state = state;
        }

        public string State
        {
            get
            {
                return _state;
            }
        }
    }
    /// <summary>
    /// 发起人类
    /// </summary>
    public class Originator
    {
        public string State
        { get; set; }

        /// <summary>
        /// 创建新的备忘录类，并保留当前状态
        /// </summary>
        /// <returns></returns>
        public Memento CreateMemento()
        {
            return new Memento(State);
        }

        public void SetMemento(Memento memento)
        {
            State = memento.State;
        }
        public void Show()
        {
            Console.WriteLine($"State={State}");
        }

    }
    /// <summary>
    /// 管理者类，保留原备忘录类
    /// </summary>
    public class Caretaker
    {

        public Memento Memento
        {
            get; set;
        }
    }
}
