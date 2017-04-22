using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 组合模式
    * 将对象组合成树形结构以表示“部分-整体”的层次结构。组合模式使得用户对单个对象和组合对象的使用具有一致性
    ****************************************************************************/

    public abstract class ComponentGroup
    {
        string _name;
        public ComponentGroup(string name)
        {
            _name = name;
        }
        public abstract void Add(ComponentGroup cg);
        public abstract void Remove(ComponentGroup cg);
        public abstract void Display(int depth);

    }

    public class Leaf : ComponentGroup
    {
        string _name;
        public Leaf(string name) : base(name)
        {
            _name = name;
        }

        public override void Add(ComponentGroup cg)
        {
            Console.WriteLine("Leaf.Add");
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + _name);
        }

        public override void Remove(ComponentGroup cg)
        {
            Console.WriteLine("Leaf.Remove");
        }
    }

    public class Composite : ComponentGroup
    {
        List<ComponentGroup> children = new List<ComponentGroup>();

        string _name;
        public Composite(string name) : base(name)
        {
            _name = name;
        }

        public override void Add(ComponentGroup cg)
        {
            children.Add(cg);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + _name);
            foreach (ComponentGroup cg in children)
            {
                cg.Display(depth + 2);
            }
        }

        public override void Remove(ComponentGroup cg)
        {
            children.Remove(cg);
        }
    }
}
