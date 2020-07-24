
using Microsoft.Extensions.Caching.Memory;
using Polly;
using System;

using System.Threading;
using System.Threading.Tasks;

namespace PollyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Bulkhead();
        }


        #region Retry

        public static void Retry()
        {
            try
            {
                //定义捕捉到的异常类型后重试
                var policy = Policy
                    .Handle<DivideByZeroException>()
                    .Or<Exception>()
                    //重试三次
                    .Retry(3, (excetpion, index, context) =>
                    {
                        Console.WriteLine(excetpion.Message);
                        Console.WriteLine(index);
                        Console.WriteLine(context);
                    });

                //开始执行
                policy.Execute(() =>
                {
                    Console.WriteLine("开始");
                    throw new IndexOutOfRangeException("越界");
                });
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Excepiton:{exc.Message}");
            }
            Console.ReadLine();
        }

        #endregion

        #region Fallback
        static void Fallback()
        {
            //在遇到故障时指定一个默认的返回值，
            var policy =
                Policy<string>
                    .Handle<Exception>()
                    .Fallback("执行失败，返回Fallback");

            var fallBack = policy.Execute(() =>
            {
                throw new Exception("我是异常！");
            });
            Console.WriteLine(fallBack);
        }
        #endregion

        #region CircuitBreaker

        static void CircuitBreaker1()
        {

            Action<Exception, TimeSpan, Context> onBreak = (exception, timespan, context) =>
            {
                Console.WriteLine($"onBreak:{exception.Message} timespan:{timespan.TotalSeconds}s, context:{context.Count}");
            };
            Action<Context> onReset = (context) =>
            {
                Console.WriteLine($"onReset, context:{context.Count}");
            };
            var breaker = Policy
                .Handle<DivideByZeroException>()
                //测试三次，熔断后5秒后再重试
                .CircuitBreaker(5, TimeSpan.FromSeconds(10), onBreak, onReset);

            var times = 0;
            while (true)
            {
                try
                {
                    times++;
                    breaker.Execute(() =>
                    {
                        Thread.Sleep(500);
                        Console.WriteLine($"开始执行,次数：{times}");
                        throw new DivideByZeroException($"除以0,次数：{times}");
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine($"异常,Message:({e.Message})");
                }
                Thread.Sleep(500);
            }

        }
        static void CircuitBreaker2()
        {
            Action<Exception, TimeSpan, Context> onBreak = (exception, timespan, context) =>
            {
                Console.WriteLine($"onBreak:{exception.Message} timespan:{timespan.TotalSeconds}s, context:{context.Count}");
            };
            Action<Context> onReset = (context) =>
            {
                Console.WriteLine($"onReset, context:{context.Count}");
            };
            var breaker = Policy
                .Handle<Exception>()
                .AdvancedCircuitBreaker(
                  failureThreshold: 0.5, //0.5为>=50%, 取值0~1，熔断的比例
                  samplingDuration: TimeSpan.FromSeconds(10), //基础收集时间为10s
                  minimumThroughput: 8, //10s超过8次触发熔断
                  durationOfBreak: TimeSpan.FromSeconds(5) //熔断间隔
                );
            var times = 0;
            while (true)
            {
                try
                {
                    times++;
                    breaker.Execute(() =>
                    {
                        Thread.Sleep(500);
                        if (times % 2 == 0)
                        {
                            Console.WriteLine($"次数：{times}，有异常！");
                            throw new Exception("除以0");
                        }
                        Console.WriteLine($"次数：{times}，无异常！");
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine($"异常,Message:({e.Message})");
                }
                Thread.Sleep(500);
            }

        }
        #endregion

        #region WaitAndRetry

        static void WaitAndRetry()
        {

            try
            {
                //时间段数组
                //var politicaWaitAndRetry = Policy
                //  .Handle<Exception>()
                //  .WaitAndRetry(new[]
                //  {
                //        TimeSpan.FromSeconds(1),
                //        TimeSpan.FromSeconds(3),
                //        TimeSpan.FromSeconds(5),
                //        TimeSpan.FromSeconds(7)
                //  }, (exception,timespan,count,context)=> {
                //      Console.WriteLine($"异常:{exception.Message}, {count:00} (调用秒数: {timespan.Seconds} 秒)\t执行时间: {DateTime.Now}");
                //  });
                //次数+时间间隔
                var politicaWaitAndRetry = Policy
                    .Handle<Exception>()
                    //重试5次
                    .WaitAndRetry(5
                    //第次间隔3s,count为重试的索引
                    , sleepDurationProvider: (count) =>
                    {
                        Console.WriteLine($"count={count}");
                        return TimeSpan.FromSeconds(3);
                    }
                    , onRetry: (exception, timespan, count, context) =>
                    {
                        Console.WriteLine($"异常:{exception.Message}, {count:00} (调用秒数: {timespan.Seconds} 秒)\t执行时间: {DateTime.Now}");
                    });
                politicaWaitAndRetry.Execute(() =>
                {
                    throw new Exception("我的异常");
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"异常,Message:({e.Message})");
            }

        }


        static void WaitAndRetryForever()
        {
            try
            {
                var politicaWaitAndRetry = Policy
                  .Handle<Exception>()
                  .WaitAndRetryForever(retryAttempt =>
                  {
                      Console.WriteLine($"retryAttempt={retryAttempt}");
                      return TimeSpan.FromSeconds(3);
                  }, (exception, timespan) =>
                  {
                      Console.WriteLine($"异常:{exception.Message}, (调用秒数: {timespan.Seconds} 秒)\t执行时间: {DateTime.Now}");
                  });
                politicaWaitAndRetry.Execute(() =>
                {
                    throw new Exception("我的异常");
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"异常,Message:({e.Message})");
            }

        }
        #endregion

        #region 组合
        static void Group()
        {
            var fallBackPolicy =
                Policy<string>
                    .Handle<Exception>()
                    .Fallback("执行失败，返回Fallback");

            var waitAetryPolicy =
                Policy<string>
                    .Handle<Exception>()
                    .WaitAndRetry(3, (count) =>
                    {
                        Console.WriteLine($"执行失败! 重试次数 {count}");
                        return TimeSpan.FromSeconds(3);
                    });

            var mixedPolicy = Policy.Wrap(fallBackPolicy, waitAetryPolicy);
            var mixedResult = mixedPolicy.Execute(() =>
            {
                throw new Exception("组合");
            });
            Console.WriteLine($"执行结果: {mixedResult}");
        }
        #endregion

        #region cache
        public static void Cache()
        {
            var mc = new MemoryCache(new MemoryCacheOptions() { });
            var memoryCacheProvider = new Polly.Caching.MemoryCache.MemoryCacheProvider(mc);
            var policy = Policy.Cache(memoryCacheProvider, TimeSpan.FromSeconds(5));
            var context = new Context("key");
            for (int i = 0; i < 10; i++)
            {
                var cache = policy.Execute(cxt =>
                {
                    Console.WriteLine("执行" + cxt.CorrelationId);
                    return DateTime.Now;
                }, context);
                Console.WriteLine(cache);
                Thread.Sleep(1000);
            }
        }
        #endregion

        #region TimeOut
        public static void TimeOut()
        {
            try
            {
                var policy = Policy.Timeout(TimeSpan.FromSeconds(3));
                var cts = new CancellationTokenSource();
                policy.Execute(ct =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Thread.Sleep(1000);
                        Console.WriteLine(i);
                        ct.ThrowIfCancellationRequested();
                    }
                }, cts.Token);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        #endregion

        #region Bulkhead Isolation
        public static void Bulkhead()
        {
            try
            {                
                var policy = Policy.Bulkhead(5,2, context => {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "---" + context.CorrelationId);
                });
                var arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                Parallel.ForEach(arr, (p) =>
                {
                    try
                    {
                        //开始执行
                        policy.Execute(() =>
                        {
                            Console.WriteLine($"{DateTime .Now}  元素:{p}  线程ID:{Thread.CurrentThread.ManagedThreadId}");
                        });
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("内部:" + exc.Message);
                    }

                });
                //for (int i = 0; i < 10; i++)
                //{
                //    new Thread(() =>
                //    {
                //        try
                //        {
                //            //开始执行
                //            policy.Execute(() =>
                //            {
                //                Console.WriteLine("开始" + Thread.CurrentThread.ManagedThreadId + "---" + DateTime.Now);
                //            });
                //        }
                //        catch(Exception exc)
                //        {
                //            Console.WriteLine("内部:"+exc.Message);
                //        }
                //    }).Start();
                //}
            }
            catch (Exception exc)
            {
                Console.WriteLine($"{exc.GetType().FullName}   Excepiton:{exc.Message}");
            }
            Console.ReadLine();
        }
        #endregion

    }
}
