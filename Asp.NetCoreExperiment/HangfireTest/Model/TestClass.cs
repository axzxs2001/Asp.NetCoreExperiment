using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireTest.Model
{
    public class TestClass
    {
        public static void Once()
        {
            Console.WriteLine("---------------单次执行-------------");
        }
        public static void Once(string json)
        {
            Console.WriteLine($"---------------单次执行-------------参数：{json}");
        }
        public void Once1(string json)
        {
            Console.WriteLine($"---------------单次执行-------------实例化，参数：{json}");
        }

        public static void Dealy()
        {
            Console.WriteLine("---------------延迟执行-------------");
        }
        public static void Dealy(string json)
        {
            Console.WriteLine($"---------------延迟执行-------------参数：{json}");
        }
        public  void Dealy1(string json)
        {
            Console.WriteLine($"---------------延迟执行-------------实例化，参数：{json}");
        }
        public static void Cycle()
        {
            Console.WriteLine("---------------周期执行-------------");
        }
        public static void Cycle(string json)
        {
            Console.WriteLine($"---------------周期执行-------------参数：{json}");
        }
        public  void Cycle1(string json)
        {
            Console.WriteLine($"---------------周期执行-------------实例化，参数：{json}");
        }
    }
}
