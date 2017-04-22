using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 适配器模式
    * 将一个类的接口转换成客户希望的另外一个接口。适配模式使得原本由于接口不兼容而不能一起工作的那些类可以一起工作。
    ****************************************************************************/
    /// <summary>
    /// 原期待类
    /// </summary>
    public  class Target
    {
        public virtual void Request()
        {
            Console.WriteLine("Target.Request");
        }
    }
    /// <summary>
    /// 需要适配的类
    /// </summary>
    public class Adaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("Adaptee.SpecificRequest");
        }
    }
    /// <summary>
    /// 适配类
    /// </summary>
    public class Adapter:Target
    {
        Adaptee adaptee = new Adaptee();
        public override void Request()
        {
            adaptee.SpecificRequest();
            Console.WriteLine("Adapter.Request");
        }
    }
}
