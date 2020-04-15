using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipelineModelDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            PipelineBuilderTest();
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// 调用事例，便Starup中的Configure
        /// </summary>
        public static void PipelineBuilderTest()
        {
            var requestContext = new RequestContext()
            {
                RequesterName = "Kangkang",
                Hour = 12,
            };

            var builder = PipelineBuilder.Create<RequestContext>(context =>
            {
                Console.WriteLine($"{context.RequesterName} {context.Hour}h apply failed");
            })
                    .Use((context, next) =>
                    {
                        Console.WriteLine("<=2");
                        if (context.Hour <= 2)
                        {
                            Console.WriteLine("pass 1");
                        }
                        else
                        {
                            next();
                        }
                    })
                    .Use((context, next) =>
                    {
                        Console.WriteLine("<=4");
                        if (context.Hour <= 4)
                        {
                            Console.WriteLine("pass 2");
                        }
                        else
                        {
                            next();
                        }
                    })
                    .Use((context, next) =>
                    {
                        Console.WriteLine("<=6");
                        if (context.Hour <= 6)
                        {
                            Console.WriteLine("pass 3");
                        }
                        else
                        {
                            next();
                        }
                    })
                ;
            var requestPipeline = builder.Build();
            foreach (var i in Enumerable.Range(1, 8))
            {
                Console.WriteLine();
                Console.WriteLine($"{i}、--------- h:{i} apply Pipeline------------------");
                requestContext.Hour = i;
                requestPipeline.Invoke(requestContext);
                Console.WriteLine("----------------------------");
                Console.WriteLine();
            }
        }

        public static async Task AsyncPipelineBuilderTest()
        {
            var requestContext = new RequestContext()
            {
                RequesterName = "Michael",
                Hour = 12,
            };

            var builder = PipelineBuilder.CreateAsync<RequestContext>(context =>
            {
                Console.WriteLine($"{context.RequesterName} {context.Hour}h apply failed");
                return Task.CompletedTask;
            })
                    .Use(async (context, next) =>
                    {
                        if (context.Hour <= 2)
                        {
                            Console.WriteLine("pass 1");
                        }
                        else
                        {
                            await next();
                        }
                    })
                    .Use(async (context, next) =>
                    {
                        if (context.Hour <= 4)
                        {
                            Console.WriteLine("pass 2");
                        }
                        else
                        {
                            await next();
                        }
                    })
                    .Use(async (context, next) =>
                    {
                        if (context.Hour <= 6)
                        {
                            Console.WriteLine("pass 3");
                        }
                        else
                        {
                            await next();
                        }
                    })
                ;
            var requestPipeline = builder.Build();
            foreach (var i in Enumerable.Range(1, 8))
            {
                Console.WriteLine($"--------- h:{i} apply AsyncPipeline------------------");
                requestContext.Hour = i;
                await requestPipeline.Invoke(requestContext);
                Console.WriteLine("----------------------------");
            }
        }

    }

    /// <summary>
    /// Demo上下文
    /// </summary>
    class RequestContext
    {
        public string RequesterName { get; set; }

        public int Hour { get; set; }
    }


    /// <summary>
    /// 没有返回值的同步中间件构建器
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IPipelineBuilder<TContext>
    {
        IPipelineBuilder<TContext> Use(Func<Action<TContext>, Action<TContext>> middleware);

        Action<TContext> Build();



    }

    /// <summary>
    /// 异步中间件构建器
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IAsyncPipelineBuilder<TContext>
    {
        IAsyncPipelineBuilder<TContext> Use(Func<Func<TContext, Task>, Func<TContext, Task>> middleware);

        Func<TContext, Task> Build();
    }

    /// <summary>
    /// 构建Pipeline类，用Create实现
    /// </summary>
    public class PipelineBuilder
    {
        public static IPipelineBuilder<TContext> Create<TContext>(Action<TContext> completeAction)
        {
            return new PipelineBuilder<TContext>(completeAction);
        }

        public static IAsyncPipelineBuilder<TContext> CreateAsync<TContext>(Func<TContext, Task> completeFunc)
        {
            return new AsyncPipelineBuilder<TContext>(completeFunc);
        }
    }
    /// <summary>
    /// 实现IPipelineBuilder接口
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    internal class PipelineBuilder<TContext> : IPipelineBuilder<TContext>
    {
        private readonly Action<TContext> _completeFunc;
        private readonly IList<Func<Action<TContext>, Action<TContext>>> _pipelines;

        public PipelineBuilder(Action<TContext> completeFunc)
        {
            _pipelines = new List<Func<Action<TContext>, Action<TContext>>>();
            _completeFunc = completeFunc;
        }

        public IPipelineBuilder<TContext> Use(Func<Action<TContext>, Action<TContext>> middleware)
        {
            _pipelines.Add(middleware);
            return this;
        }

        public Action<TContext> Build()
        {
            var request = _completeFunc;
            foreach (var pipeline in _pipelines.Reverse())
            {
                request = pipeline(request);
            }
            return request;
        }
    }
    /// <summary>
    /// 实现IAsyncPipelineBuilder接口
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    internal class AsyncPipelineBuilder<TContext> : IAsyncPipelineBuilder<TContext>
    {
        private readonly Func<TContext, Task> _completeFunc;
        private readonly IList<Func<Func<TContext, Task>, Func<TContext, Task>>> _pipelines = new List<Func<Func<TContext, Task>, Func<TContext, Task>>>();

        public AsyncPipelineBuilder(Func<TContext, Task> completeFunc)
        {
            _completeFunc = completeFunc;
        }

        public IAsyncPipelineBuilder<TContext> Use(Func<Func<TContext, Task>, Func<TContext, Task>> middleware)
        {
            _pipelines.Add(middleware);
            return this;
        }

        public Func<TContext, Task> Build()
        {
            var request = _completeFunc;
            foreach (var pipeline in _pipelines.Reverse())
            {
                request = pipeline(request);
            }
            return request;
        }
    }

    /// <summary>
    /// 扩展方法，实现调用Use
    /// </summary>
    public static class TPipelineExtion
    {
        public static IPipelineBuilder<TContext> Use<TContext>(this IPipelineBuilder<TContext> builder, Action<TContext, Action> action)

        {
            return builder.Use(next =>
                               context =>
                               {
                                   action(context, () => next(context));
                               });
        }

        public static IAsyncPipelineBuilder<TContext> Use<TContext>(this IAsyncPipelineBuilder<TContext> builder, Func<TContext, Func<Task>, Task> func)
        {
            return builder.Use(next =>
                               context =>
                               {
                                   return func(context, () => next(context));
                               });
        }
    }
}
