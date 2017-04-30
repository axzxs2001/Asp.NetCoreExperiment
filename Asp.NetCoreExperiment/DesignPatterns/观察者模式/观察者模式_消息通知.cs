using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.观察者模式
{
    class 观察者模式_消息通知
    {
    }
    /// <summary>
    /// 观察者类
    /// </summary>
    public abstract class Observer
    {
        public abstract void SendMessage();
    }

    /// <summary>
    /// 观察者主体对象
    /// </summary>
    public abstract class MessageObject
    {
        /// <summary>
        /// 观察者集合
        /// </summary>
        IList<Observer> _observers;
        public MessageObject()
        {
            _observers = new List<Observer>();
        }
        /// <summary>
        /// 添加观察者
        /// </summary>
        /// <param name="observer">观察者</param>
        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }
        /// <summary>
        /// 移除观察者
        /// </summary>
        /// <param name="observer">观察者</param>
        public void Detach(Observer observer)
        {
            _observers.Remove(observer);
        }
        /// <summary>
        /// 发送通知
        /// </summary>
        public void SendMessages()
        {
            foreach (var obs in _observers)
            {
                obs.SendMessage();
            }
        }
    }
}
