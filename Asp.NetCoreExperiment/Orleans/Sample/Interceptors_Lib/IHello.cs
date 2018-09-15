using Orleans;
using Orleans.Runtime;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Interceptors_Lib
{
    public interface IHelloGrain : IGrainWithGuidKey
    {
        Task<int> GetFavoriteNumber(string name);
        Task Other();
    }
    public class HelloGrain : Grain, IHelloGrain, IIncomingGrainCallFilter
    {
        public async Task Invoke(IIncomingGrainCallContext context)
        {
            //调用方法前获取参数，调用方法后修改返回值
            //if (context.Arguments != null)
            //{
            //    foreach (var arg in context.Arguments)
            //    {
            //        Console.WriteLine($"参数:{arg}");
            //    }
            //    if (context.Arguments.Length > 0)
            //    {
            //        context.Arguments[0] = "桂素伟加一";
            //    }
            //}
            //await context.Invoke();
            //// Change the result of the call from 7 to 38.
            //if (string.Equals(context.InterfaceMethod.Name, nameof(this.GetFavoriteNumber)))
            //{
            //    context.Result = 38;
            //}

            //用特性的方式控制访问
            var isAdminMethod = context.ImplementationMethod.GetCustomAttribute<AdminOnlyAttribute>() != null;
            if (isAdminMethod && !(bool)RequestContext.Get("isAdmin"))
            {
                throw new Exception($"只有 admins 能访问 {context.ImplementationMethod.Name}!");
            }
            await context.Invoke();
        }
        [AdminOnly]
        public Task<int> GetFavoriteNumber(string name)
        {
            Console.WriteLine($"GetFavoriteNumber方法内收到的参数为：{name}");
            return Task.FromResult(7);
        }


        public Task Other()
        {
            Console.WriteLine("调用了Other");
            return Task.CompletedTask;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class AdminOnlyAttribute : Attribute { }
}
