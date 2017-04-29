using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /*********************************************************************
     * 动态地给一个对象添加一些客外的职责，就增加功能来说，装饰模式比生成子类更为灵活
     *********************************************************************/
    /// <summary>
    /// 组件抽象类
    /// </summary>
    public abstract class Component
    {
        public abstract void Operation();
    }
    /// <summary>
    /// 功能一子类
    /// </summary>
    public class ConcreteComponent : Component
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteComponent.Operation");
        }
    }
    /// <summary>
    /// 装饰类
    /// </summary>
    public class Decorator : Component
    {
        public Component Component
        {
            set;
            protected get;
        }
        public override void Operation()
        {
            if (Component != null)
            {
                Component.Operation();
                Console.WriteLine("Decorator.Operation");
            }
        }
    }
    /// <summary>
    /// 装饰功能子类A
    /// </summary>
    public class ConcreteDecoratorA : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("ConcreteDecoratorA.Operation");
            //A();
        }
        void A()
        {
            Console.WriteLine("ConcreteDecoratorA.A");
        }
    }
    /// <summary>
    /// 装饰功能子类B
    /// </summary>
    public class ConcreteDecoratorB : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("ConcreteDecoratorB.Operation");
           // B();
        }
        void B()
        {
            Console.WriteLine("ConcreteDecoratorB.B");
        }
    }

}
