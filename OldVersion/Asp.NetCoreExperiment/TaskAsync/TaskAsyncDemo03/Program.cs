using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsyncDemo03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task   With Thread  Start !");
            for (int i = 0; i <= 15; i++)
            {
                var t = new Thread(Dotaskfunction);
                t.Start();
            }
            Console.WriteLine("Task   With Thread End !");

            Console.WriteLine("Task   With Task   Start !");
            for (int i = 0; i <= 15; i++)
            {
                //从线程池中取线程执行
                Task.Run(() => { Dotaskfunction1(); });
            }
            Console.WriteLine("Task   With Task End !");
            Console.ReadLine();
        }
        public static void Dotaskfunction()
        {
            Console.WriteLine("Thread  has been done! ThreadID: {0},IsBackGround:{1} ", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsBackground);

        }
        public static void Dotaskfunction1()
        {
            Console.WriteLine("task  has been done! ThreadID: {0},IsBackGround:{1} ", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsBackground);

        }
    }
}
