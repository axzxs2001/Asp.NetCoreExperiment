using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 桥接模式
    * 将抽象部分与它的实现部分分离，使它们都可以独立地变化。
    ****************************************************************************/
    public abstract class Implementor
    {
        public abstract void Operation();
    }

    public class ImplementorA : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("ImplementorA.Operation");
        }
    }
    public class ImplementorB : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("ImplementorB.Operation");
        }
    }
    /// <summary>
    /// 抽象类
    /// </summary>
    public class Abstraction
    {
        protected Implementor _implementor;
        public void SetImplementor(Implementor implementor)
        {
            _implementor = implementor;
        }

        public virtual void Operation()
        {
            _implementor.Operation();
        }
    }
    /// <summary>
    /// 被提炼的抽象
    /// </summary>
    public class RefinedAbstraction: Abstraction
    {
        public override void Operation()
        {
            _implementor.Operation();
        }
    }
}
