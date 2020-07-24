using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsyncDemo05
{
    class Program
    {
        static void Main(string[] args)
        {
            // FAsync().Wait();//异常会报出来
            var t = FAsync();

            Console.WriteLine("Main");
           //这个时候异常还没有抛出来
            Console.WriteLine(t.Exception != null ? t.Exception.Message : "无");
            Console.WriteLine(t.Exception != null&&t.Exception.InnerException!=null ? t.Exception.InnerException.Message : "无");
            Console.ReadLine();
            //等待一会，异常就会抛出来了
            Console.WriteLine(t.Exception != null ? t.Exception.Message : "无");
            Console.WriteLine(t.Exception != null && t.Exception.InnerException != null ? t.Exception.InnerException.Message : "无");
            Console.ReadLine();
        }

        static async Task FAsync()
        {
            Console.WriteLine($"开始:{DateTime.Now.ToString("HH:mm:ss.fffffff")}");
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"异常抛出来了，可以回车了");               
                throw new Exception("FAsync异常");
            });

            Console.WriteLine($"结束:{DateTime.Now.ToString("HH:mm:ss.fffffff")}");
        }
    }
}
