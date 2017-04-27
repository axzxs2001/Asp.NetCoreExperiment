using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.装饰模式
{
    class 装饰模式_A
    {
        public static void Start()
        {
            var a = new A();
            DecoratorA decA = new DecoratorA();
            DecoratorB decB = new DecoratorB();
            decA.A = a;
            decB.A = decA;
            decB.F();
        }
    }

    public class A
    {
        public virtual void F()
        {
            Console.WriteLine("A.F");
        }
    }
    public class Decorator : A
    {
        /// <summary>
        /// 用属性来传递
        /// </summary>
        public A A
        {
            set;
            protected get;
        }
        /// <summary>
        /// 用方法来扩展和调用父方法，实现串联
        /// </summary>
        public override void F()
        {
            Console.WriteLine("Decorator.F");
            base.F();
        }
    }

    public class DecoratorA : Decorator
    {
        public override void F()
        {
            Console.WriteLine("DecoratorA.F");
            base.F();
        }
    }
    public class DecoratorB : Decorator
    {
        public override void F()
        {
            Console.WriteLine("DecoratorB.F");
            base.F();
        }
    }
}
