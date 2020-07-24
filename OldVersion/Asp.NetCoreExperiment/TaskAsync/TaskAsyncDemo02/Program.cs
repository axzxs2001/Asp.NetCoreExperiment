using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsyncDemo02
{
    class Program
    {
        static async Task Main(string[] args)
        {
           
            //AsyncF().GetAwaiter().GetResult();  //输出顺序 bbb ccc aaa
            //await AsyncF();  //输出顺序  bbb ccc aaa
            //AsyncF().GetAwaiter();//输出顺序  aaa bbb ccc
            //AsyncF();//输出顺序  aaa bbb ccc
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("aaa");
            Console.ReadLine();
        }
        //static  void Main(string[] args)
        //{
        //    //AsyncF().GetAwaiter().GetResult();  //输出顺序 bbb ccc aaa
        //    AsyncF().GetAwaiter();  //输出顺序 aaa bbb ccc 
        //   // AsyncF();//输出顺序  aaa bbb ccc
        //    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        //    Console.WriteLine("aaa");
        //    Console.ReadLine();
        //}
        static async Task AsyncF()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(500);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("bbb");
            });
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("ccc");
        }
    }
}
