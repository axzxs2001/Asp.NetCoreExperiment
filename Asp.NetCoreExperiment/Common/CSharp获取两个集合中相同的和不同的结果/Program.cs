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
            Console.WriteLine("-------------------开始初始化集合");
            var sw = new Stopwatch();
            sw.Start();          
            var listA = new List<string>();
            var listB = new List<string>();
            for (int i = 0; i < 100000; i++)
            {
                listA.Add("abcdefghigklmn12345" + i);
            }
            for (int i = 0; i < 100000; i++)
            {
                listB.Add("abcdefghigklmn12345" + (i + 25));
            }
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);

            Console.WriteLine("-------------------开始比较");
            //Console.WriteLine("listA集合");
            //foreach (var item in listA)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine("listB集合");
            //foreach (var item in listB)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine("listA和listB交集");
            sw = new Stopwatch();
            sw.Start();
            var listC = listA.Intersect(listB);
            sw.Stop();
            ts2 = sw.Elapsed;
            Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);
            //Console.WriteLine($"----------------相同的有：{listC.Count()}--------------------");
            //foreach (var item in listC)
            //{
            //    Console.WriteLine(item);
            //}
            Console.WriteLine("-------------------listA中交集外---------------------");
            sw = new Stopwatch();
            sw.Start();
            var listD = listA.Except(listA.Intersect(listB));
            sw.Stop();
            ts2 = sw.Elapsed;
            Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);
            foreach (var item in listD)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----------------------listB中交集外--------------------");
            sw = new Stopwatch();
            sw.Start();
            var listE = listB.Except(listA.Intersect(listB));
            sw.Stop();
            ts2 = sw.Elapsed;
            Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);
            foreach (var item in listE)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("--------------listA和listB中交集外-------------------");
            sw = new Stopwatch();
            sw.Start();
            var listF = listA.Concat(listB).Except(listA.Intersect(listB));
            sw.Stop();
            ts2 = sw.Elapsed;
            Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);
            foreach (var item in listF)
            {
                Console.WriteLine(item);
            }
        }
    }
}
