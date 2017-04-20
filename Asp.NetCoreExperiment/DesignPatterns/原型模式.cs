using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 原型模式
    * 用原型实例指定创建对象的种类，并且通过拷贝这些原型创建新对象。
    ****************************************************************************/
    /// <summary>
    /// 抽象类
    /// </summary>
    public abstract class Prototype
    {       
        public Prototype(string id)
        {
            ID = id;
        }
        public string ID
        {
            get;
            private set;
        }
        public abstract Prototype Clone();
    }
    /// <summary>
    /// 子类
    /// </summary>
    public class ConcretePrototype1 : Prototype
    {
        public ConcretePrototype1(string id) : base(id)
        {
        }
        public override Prototype Clone()
        {
            Console.WriteLine("ConcretePrototype1.Clone");
            return (Prototype)this.MemberwiseClone();
        }
    }
}
