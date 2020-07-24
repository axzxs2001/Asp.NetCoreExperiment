using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsyncDemo01
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //异常等待
            for (int i = 0; i < 1000; i++)
            {
                await FAsync(i);
            }

            //////异步不等待
            //for (int i = 0; i < 1000; i++)
            //{
            //    FAsync(2000 + i);
            //}

            ////等待
            //for (int i = 0; i < 6; i++)
            //{
            //    F(8000 + i);
            //}
            Console.ReadLine();
        }


        static async Task FAsync(int i)
        {
            try
            {
                Console.WriteLine("开始" + i);
                var con = new SqlConnection($"server=.;uid=sa;pwd=sa;database=starpay{(i % 3 == 0 ? i.ToString() : "")};max pool size=1000");
                await con.OpenAsync();
                Console.WriteLine("结束：" + i);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"{i}     { exc.Message}");
            }
        }

        static void F(int i)
        {
            try
            {
                Console.WriteLine("开始" + i);
                var con = new SqlConnection($"server=.;uid=sa;pwd=sa;database=starpay{(i % 3 == 0 ? "1" : "")};max pool size=1000");
                con.Open();
                Console.WriteLine("结束：" + i);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
