using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;

namespace CollectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SortListTest();
            SortDictionaryTest();
        }
        /// <summary>
        /// 只读List
        /// </summary>
        static void ReadOnlyListTest()
        {
            IReadOnlyList<string> readOnlyList = new List<string>() { "a", "b", "c" };
            foreach (var item in readOnlyList)
            {
                Console.WriteLine(item);
            }
            /*输出结果
             a
             b
             c
             */
        }
        /// <summary>
        /// 只读字典
        /// </summary>
        static void ReadOnlyDictionaryTest()
        {
            var readOnlyDictionary = new ReadOnlyDictionary<int, string>(
                new Dictionary<int, string>
                {
                    {5,"五"},
                    {1,"一"},
                    {10, "十"}
                });

            foreach (var item in readOnlyDictionary)
            {
                Console.WriteLine($"{item.Key}~{item.Value}");
            }
            /*输出结果
             5~五
             1~一
             10~十
             */
        }

        /// <summary>
        /// 排序列表
        /// </summary>
        static void SortListTest()
        {
            var sortList = new SortedList<int, string>();
            sortList.Add(10, "十");
            sortList.Add(5, "五");
            sortList.Add(1, "一");
            Console.WriteLine(sortList.Keys);
            foreach (var item in sortList)
            {
                Console.WriteLine($"{item.Key}~{item.Value}");
            }
            /*输出结果
             1~一
             5~五
             10~十
             */
        }
        /// <summary>
        /// 排序字典
        /// </summary>
        static void SortDictionaryTest()
        {
            var sortDic = new SortedDictionary<int, string>();
            sortDic.Add(10, "十");
            sortDic.Add(5, "五");
            sortDic.Add(1, "一");
            Console.WriteLine(sortDic.Keys);
            foreach (var item in sortDic)
            {
                Console.WriteLine($"{item.Key}~{item.Value}");
            }
            /*输出结果
             1~一
             5~五
             10~十
            */
        }
        /// <summary>
        /// 排序set，不含重复值
        /// </summary>
        static void SortSetTest()
        {
            var sortSet = new SortedSet<int>();
            sortSet.Add(10);
            sortSet.Add(5);
            sortSet.Add(1);
            sortSet.Add(1);
            foreach (var item in sortSet)
            {
                Console.WriteLine(item);
            }
            /*输出结果
             1
             5
             10
            */
        }

        /// <summary>
        /// 链表：每个元素承上启下
        /// </summary>
        static void LinkedListTest()
        {
            var linkedList = new LinkedList<string>();
            linkedList.AddLast("2");
            linkedList.AddLast("3");
            linkedList.AddLast("5");

            linkedList.AddFirst("1");

            linkedList.AddBefore(linkedList.Find("5"), "4");
            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"2前面的值:{linkedList.Find("2").Previous.Value}");
            Console.WriteLine($"2后面的值:{linkedList.Find("2").Next.Value}");

            /*输出结果
             1
             2
             3
             4
             5
             2前面的值:1
             2后面的值:3
             */
        }
        /// <summary>
        /// 哈希集合
        /// </summary>
        static void HashSetTest()
        {
            var hashSet = new HashSet<string>();
            hashSet.Add("a");
            hashSet.Add("c");
            hashSet.Add("b");
            hashSet.Add("a");
            hashSet.Add("c");
            hashSet.Add("b");
            foreach (var item in hashSet)
            {
                Console.WriteLine(item);
            }
            /*输出结果
             a
             b
             c
             */
        }
        /// <summary>
        /// 队列：先进先出
        /// </summary>
        static void QueueTest()
        {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            foreach (var item in queue)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"dequeue元素：{queue.Dequeue()}");
            /*输出结果
             1
             2
             3
             dequeue元素：1
             */
        }
        /// <summary>
        /// 堆栈：后进先出
        /// </summary>
        static void StackTest()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
            //pop元素
            Console.WriteLine($"pop元素:{stack.Pop()}");
            /*输出结果
             3
             2
             1
             pop元素：3
             */
        }
    }

}
