using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 访问者模式
    * 表示一个作用于某对象结构中的各元素的操作。它使你可以在不改变各元素的前提下定义作用于这些元素的新操作
    ****************************************************************************/

    /// <summary>
    /// 访问者抽象类
    /// </summary>
    public abstract class Visitor
    {
        public abstract void VistConcreteElementA(ConcreteElementA element);
        public abstract void VistConcreteElementB(ConcreteElementB element);
    }

    public class ConcreteVisitor1 : Visitor
    {
        public override void VistConcreteElementA(ConcreteElementA element)
        {
            Console.WriteLine($"element:{element.GetType().Name}  ConcreteVisitor1.VistConcreteElementA");
        }

        public override void VistConcreteElementB(ConcreteElementB element)
        {
            Console.WriteLine($"element:{element.GetType().Name}  ConcreteVisitor1.VistConcreteElementB");
        }
    }
    public class ConcreteVisitor2 : Visitor
    {
        public override void VistConcreteElementA(ConcreteElementA element)
        {
            Console.WriteLine($"element:{element.GetType().Name}  ConcreteVisitor2.VistConcreteElementA");
        }

        public override void VistConcreteElementB(ConcreteElementB element)
        {
            Console.WriteLine($"element:{element.GetType().Name}  ConcreteVisitor2.VistConcreteElementB");
        }
    }
    /// <summary>
    /// 功能类抽象类,
    /// </summary>
    public abstract class Element
    {
        public abstract void Accept(Visitor visitor);

    }
    /// <summary>
    /// 功能类A
    /// </summary>
    public class ConcreteElementA : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VistConcreteElementA(this);
        }
    }
    /// <summary>
    /// 功能类B
    /// </summary>
    public class ConcreteElementB : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VistConcreteElementB(this);
        }
    }

    public class ObjectStructure
    {
        List<Element> _elements;
        public ObjectStructure()
        {
            _elements = new List<Element>();
        }
        public void Attach(Element element)
        {
            _elements.Add(element);
        }
        public void Detach(Element element)
        {
            _elements.Remove(element);
        }
        public void Accept(Visitor visitor)
        {
            foreach (var e in _elements)
            {
                e.Accept(visitor);
            }
        }
    }
}
