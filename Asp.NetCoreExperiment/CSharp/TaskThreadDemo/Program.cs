using System;
using System.Threading.Tasks;

namespace TaskThreadDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var allresult = true;
            for (int i = 1; i < 12; i++)
            {
                #region
                //Task task = new Task(async () =>
                //{
                //    await F(4);
                //    Console.WriteLine(DateTime.Now);
                //});
                //task.Start();
                //await Task.Run(async () =>
                // {
                //     await F(4);
                //     Console.WriteLine(DateTime.Now);
                // });
                #endregion
                await Task.Factory.StartNew(async () =>
                 {
                     var result = await F(i);
                     allresult = result && allresult;
                     Console.WriteLine("结果："+ result +"  "+DateTime.Now);
                 });
            }
            await Console.Out.WriteLineAsync(allresult.ToString());
            //Console.WriteLine(allresult);
            Console.ReadKey();
        }

        static async Task<bool> F(int i)
        {
            try
            {
                Console.WriteLine($"F开始 i={i}");
                await Task.Delay(i * 1000);
                if (i % 8 == 0)
                {
                    throw new Exception("i=3异常");
                }
                Console.WriteLine($"F结束 i={i}");
                return await Task.FromResult(true);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"F异常 i={i} {exc.Message}");
                return await Task.FromResult(false);
            }
        }
    }
}
