using Orleans;
using System;
using System.Threading.Tasks;

namespace Interceptors_Lib
{
    public class LoggingCallFilter : IIncomingGrainCallFilter
    {

        public async Task Invoke(IIncomingGrainCallContext context)
        {
            try
            {
                await context.Invoke();
                var msg = $"调用方法为： {context.Grain.GetType()}.{context.InterfaceMethod.Name}({(context.Arguments != null ? string.Join(", ", context.Arguments) : "")}) 返回值为： {context.Result}";
                Console.WriteLine(msg);
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
