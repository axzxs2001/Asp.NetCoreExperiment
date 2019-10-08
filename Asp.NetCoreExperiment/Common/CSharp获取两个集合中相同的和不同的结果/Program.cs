using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CSharp获取两个集合中相同的和不同的结果
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }
        static void Test()
        {
            Console.WriteLine("-------------------开始初始化集合-------------------");
            #region 制造数据
            var watch = new Stopwatch();
            watch.Start();
            var listA = new List<string>();
            var listB = new List<string>();
            for (int i = 0; i < 1000000; i++)
            {
                var id = Guid.NewGuid().ToString();
                listA.Add(id);
            }
            listB.AddRange(listA);
            //制造B的差异数据
            for (int i = 0; i < 10; i++)
            {
                var tick = DateTime.Now.Ticks;
                var random = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                var index = random.Next(1, 100000);
                listB[index] = Guid.NewGuid().ToString();
            }
            watch.Stop();
            TimeSpan span = watch.Elapsed;
            Console.WriteLine("制造数据总共花费{0}ms.", span.TotalMilliseconds);
            #endregion

            #region 比较
            Console.WriteLine("-------------------开始比较-------------------");
            Console.WriteLine("-------------------listA中交集外---------------------");
            watch = new Stopwatch();
            watch.Start();
            var listD = listA.Except(listA.Intersect(listB));
            watch.Stop();
            span = watch.Elapsed;
            Console.WriteLine("listA中交集外 总共花费{0}ms.", span.TotalMilliseconds);
            foreach (var item in listD)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----------------------listB中交集外--------------------");
            watch = new Stopwatch();
            watch.Start();
            var listE = listB.Except(listA.Intersect(listB));
            watch.Stop();
            span = watch.Elapsed;
            Console.WriteLine("listB中交集外 总共花费{0}ms.", span.TotalMilliseconds);
            foreach (var item in listE)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("--------------listA和listB中交集外-------------------");
            watch = new Stopwatch();
            watch.Start();
            var listF = listA.Concat(listB).Except(listA.Intersect(listB));
            watch.Stop();
            span = watch.Elapsed;
            Console.WriteLine("listA和listB中交集外 总共花费{0}ms.", span.TotalMilliseconds);
            foreach (var item in listF)
            {
                Console.WriteLine(item);
            }
            #endregion
        }
    }
}
