using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /**************************************************************
    * 代理模式
    * 为其他对象提供一种代理以控制对这个对象的访问
    **************************************************************/

    /// <summary>
    /// 抽象父类
    /// </summary>
    public abstract class Subject
    {
        public abstract void Request();
    }
    /// <summary>
    /// 真实功能类
    /// </summary>
    public class RealSubject : Subject
    {
        public override void Request()
        {
            Console.WriteLine("RealSubject.Request");
        }
    }
    /// <summary>
    /// 代理类
    /// </summary>
    public class Proxy : Subject
    {
        RealSubject _realSubject;
        public override void Request()
        {
            if (_realSubject == null)
            {
                _realSubject = new RealSubject();
            }
            Console.WriteLine("Proxy.Request");
            _realSubject.Request();
        }
    }
}
