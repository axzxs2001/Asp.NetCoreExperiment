using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 享元模式
    * 运用共享技术有效地支持大量细料度对象
    ****************************************************************************/
    /// <summary>
    /// 享元类
    /// </summary>
    public abstract class Flyweight
    {
        public abstract void Operation(int extrinsicstate);
    }
    /// <summary>
    /// 具体享元类
    /// </summary>
    public class ConcreteFlyweight : Flyweight
    {
        public override void Operation(int extrinsicstate)
        {
            Console.WriteLine($"具体Flyweight:{extrinsicstate}");
        }
    }
    /// <summary>
    /// 不享元类
    /// </summary>
    public class UnSharedConcreteFlyweight : Flyweight
    {
        public override void Operation(int extrinsicstate)
        {
            Console.WriteLine($"不共享具体Flyweight:{extrinsicstate}");
        }
    }
    public class FlyweightFactory
    {
        Dictionary<string, Flyweight> flyweights;
        public FlyweightFactory()
        {
            flyweights = new Dictionary<string, Flyweight>();
            flyweights.Add("x", new ConcreteFlyweight());
            flyweights.Add("y", new ConcreteFlyweight());
            flyweights.Add("z", new ConcreteFlyweight());
        }
        public Flyweight GetFlyweight(string key)
        {
            return flyweights[key];
        }
    }
}
