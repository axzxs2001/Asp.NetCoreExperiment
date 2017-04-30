using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 观察者模式
    * 定义了一种一对多的依赖关系，让多个观察者对象同时监听某一个主题对象。这个主题对象在状态发生变化时，会通知所有观察者对象，使它们能够自动更新自己
    ****************************************************************************/

    /// <summary>
    /// 观察者主体对象
    /// </summary>
    public abstract class ObsSubject
    {
        IList<Observer> _observers;
        public ObsSubject()
        {
            _observers = new List<Observer>();
        }

        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }
        public void Detach(Observer observer)
        {
            _observers.Remove(observer);
        }
        /// <summary>
        /// 通知
        /// </summary>
        public void Notify()
        {
            foreach (var obs in _observers)
            {
                obs.Update();
            }
        }
    }
    public class AObsSubject : ObsSubject
    {  
    }
    public class BObsSubject : ObsSubject
    {      
    }
    public abstract class Observer
    {
        public abstract void Update();
    }
    /// <summary>
    /// A观察者
    /// </summary>
    public class AObserver : Observer
    {
        ObsSubject _coSub;
        public AObserver(ObsSubject coSub)
        {
            _coSub = coSub;
        }

        public override void Update()
        {
            Console.WriteLine($"AObserver观察者更新了自己状态");
        }
    }
    /// <summary>
    /// B观察者
    /// </summary>
    public class BObserver : Observer
    {
        ObsSubject _coSub;
        public BObserver(ObsSubject coSub)
        {
            _coSub = coSub;
        }

        public override void Update()
        {
            Console.WriteLine($"BObserver观察者更新了自己状态");
        }
    }

}
