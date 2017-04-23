using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
     * 迭代器模式
     * 提供一种方法顺序访问一个聚合对象中各个元素，而又不是暴露访对象内部表示
     ****************************************************************************/

    /// <summary>
    /// 迭代器抽象类
    /// </summary>
    public abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();

        public abstract bool IsDone();
        public abstract object CurrentItem();
    }
    public class ConcreteIterator : Iterator
    {
        ConcreteAggregate aggregate;
        int current = 0;
        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            this.aggregate = aggregate;
        }
        public override object CurrentItem()
        {
            return aggregate[current];
        }

        public override object First()
        {
            return aggregate[0];
        }

        public override bool IsDone()
        {
            return current >= aggregate.Count ? true : false;
        }

        public override object Next()
        {
            object ret = null;
            current++;
            if(current<aggregate.Count)
            {
                ret = aggregate[current];                    
            }
            return ret;
        }
    }

    /// <summary>
    /// 聚集抽象类
    /// </summary>
    public abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }

    public class ConcreteAggregate : Aggregate
    {
        IList<object> items = new List<object>();
        public override Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }

        public int Count
        {
            get
            {
                return items.Count;
            }
        }
        public object this[int index]
        {
            get
            {
                return items[index];
            }
            set
            {
                items.Insert(index, value);
            }
        }
    }


}
