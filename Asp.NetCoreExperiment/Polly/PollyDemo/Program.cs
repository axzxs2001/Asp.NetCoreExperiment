using Polly;
using System;

namespace PollyDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            Demo4();
        }
        #region 默认策略

        static void Demo1()
        {
            #region demo1
            try
            {
                var retryTwoTimesPolicy = Policy
                    .Handle<DivideByZeroException>()
                    .Retry(3, (ex, count) =>
                    {
                        Console.WriteLine("执行失败! 重试次数 {0}", count);
                        Console.WriteLine("异常来自 {0}", ex.GetType().Name);
                    });
                retryTwoTimesPolicy.Execute(() =>
                {
                    Compute();
                });
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine($"异常,Message: ({e.Message})");

            }
            #endregion
        }

        static int Compute()
        {
            var a = 0;
            return 1 / a;
        }
        #endregion


        #region 重试策略

        static void Demo2()
        {
          
            try
            {
                var politicaWaitAndRetry = Policy
                  .Handle<DivideByZeroException>()
                  .WaitAndRetry(new[]
                  {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(3),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(7)
                  }, ReportaError);
                politicaWaitAndRetry.Execute(() =>
                {
                    ZeroExcepcion();
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"异常,Message:({e.Message})");
            }
        
        }
        /// <summary>
        /// 抛出异常        /// </summary>
        static void ZeroExcepcion()
        {
            throw new DivideByZeroException();
        }


        /// <summary>
        /// 异常信息        /// </summary>
        /// <param name="e"></param>
        /// <param name="tiempo"></param>
        /// <param name="intento"></param>
        /// <param name="contexto"></param>
        static void ReportaError(Exception e, TimeSpan tiempo, int intento, Context contexto)
        {
            Console.WriteLine($"异常: {intento:00} (调用秒数: {tiempo.Seconds} 秒)\t执行时间: {DateTime.Now}");
        }

        #endregion

        #region 反馈策略
        static string ThrowException()
        {
            throw new Exception();
        }
        static void Demo3()
        {
            var fallBackPolicy =
                Policy<string>
                    .Handle<Exception>()
                    .Fallback("执行失败，返回Fallback");

            var fallBack = fallBackPolicy.Execute(() =>
            {
                return ThrowException();
            });
            Console.WriteLine(fallBack);
        }
        #endregion

        #region 包裹策略
        static void Demo4()
        {
            var fallBackPolicy =
                Policy<string>
                    .Handle<Exception>()
                    .Fallback("执行失败，返回Fallback");

            var fallBack = fallBackPolicy.Execute(() =>
            {
                return ThrowException();
            });
            Console.WriteLine(fallBack);

            var politicaWaitAndRetry =
                Policy<string>
                    .Handle<Exception>()
                    .Retry(3, (ex, count) =>
                    {
                        Console.WriteLine("执行失败! 重试次数 {0}", count);
                        Console.WriteLine("异常来自 {0}", ex.GetType().Name);
                    });

            var mixedPolicy = Policy.Wrap(fallBackPolicy, politicaWaitAndRetry);
            var mixedResult = mixedPolicy.Execute(ThrowException);
            Console.WriteLine($"执行结果: {mixedResult}");
        }
        #endregion

    }
}
