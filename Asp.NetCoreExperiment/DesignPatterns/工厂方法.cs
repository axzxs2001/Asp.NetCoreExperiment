using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 工厂方法
    * 定义一个用于创建对象的接口，让子类决定实例化哪一个类。工厂方法使一个类的实例化延迟到其子类。
    ****************************************************************************/
    /// <summary>
    /// 抽象功能类
    /// </summary>
    public abstract class Function
    {
        public abstract void Operation();
    }
    /// <summary>
    /// 功能A
    /// </summary>
    public class FunctionA : Function
    {
        public override void Operation()
        {
            Console.WriteLine("FunctionA.Operation");
        }
    }
    /// <summary>
    /// 功能B
    /// </summary>
    public class FunctionB : Function
    {
        public override void Operation()
        {
            Console.WriteLine("FunctionB.Operation");
        }
    }
    /// <summary>
    /// 工厂接口
    /// </summary>
    public interface IFactory
    {
        Function CreateFuntion();
    }
    /// <summary>
    /// 工厂A
    /// </summary>
    public class FactoryA:IFactory
    {
        public Function CreateFuntion()
        {
            return new FunctionA();
        }
    }
    /// <summary>
    /// 工厂B
    /// </summary>
    public class FactoryB : IFactory
    {
        public Function CreateFuntion()
        {
            return new FunctionB();
        }
    }

}
