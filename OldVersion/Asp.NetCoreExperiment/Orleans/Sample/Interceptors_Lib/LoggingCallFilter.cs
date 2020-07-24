using Orleans;
using System;
using System.Threading.Tasks;

namespace Interceptors_Lib
{
    public class LoggingCallFilter : IIncomingGrainCallFilter
    {
        readonly IGrainFactory _grainFactory;
        public LoggingCallFilter(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }

        public async Task Invoke(IIncomingGrainCallContext context)
        {
            try
            {
                if (!(context.Grain is IHelloGrain))
                {
                    var grain = _grainFactory.GetGrain<IHelloGrain>(new Guid());

                    // Perform some grain call here.
                    await grain.Other();
                }

                await context.Invoke();
                if (context.InterfaceMethod.Name == "GetFavoriteNumber")
                {
                    var msg = $"调用方法为： {context.Grain.GetType()}.{context.InterfaceMethod.Name}({(context.Arguments != null ? string.Join(", ", context.Arguments) : "")}) 返回值为： {context.Result}";
                    Console.WriteLine(msg);
                }
            }
            catch (Exception exception)
            {
                var msg = $"{context.Grain.GetType()}.{context.InterfaceMethod.Name}({(context.Arguments != null ? string.Join(", ", context.Arguments) : "")}) 抛出了一个异常: {exception.Message}";
                Console.WriteLine(msg);
                throw;
            }
        }
    }


    public class LoggOutCallFilter : IOutgoingGrainCallFilter
    {
     

        public async Task Invoke(IOutgoingGrainCallContext context)
        {
            try
            {


                await context.Invoke();
                if (context.InterfaceMethod.Name=="GetFavoriteNumber")
                {
                    var msg = $"调用方法为： {context.Grain.GetType()}.{context.InterfaceMethod.Name}({(context.Arguments != null ? string.Join(", ", context.Arguments) : "")}) 返回值为： {context.Result}";
                    Console.WriteLine(msg);
                }



            }
            catch (Exception exception)
            {
                var msg = $"{context.Grain.GetType()}.{context.InterfaceMethod.Name}({(context.Arguments != null ? string.Join(", ", context.Arguments) : "")}) 抛出了一个异常: {exception.Message}";
                Console.WriteLine(msg);
                throw;
            }
        }
    }
}
