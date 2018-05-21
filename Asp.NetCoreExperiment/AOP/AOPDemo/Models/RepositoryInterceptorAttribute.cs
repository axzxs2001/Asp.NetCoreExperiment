using AOPDemo.Models.Repository;
using AspectCore.DynamicProxy;
using AspectCore.Injector;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AOPDemo.Models
{
    /// <summary>
    /// Repository拦截特性
    /// Nuget中安装AspectCore.Extensions.DependencyInjection
    /// 需要改造引用项目的Startup的ConfigureServices
    /// public IServiceProvider ConfigureServices(IServiceCollection services)
    /// {
    ///      services.AddTransient&lt;IARepository, ARepository&gt;();
    ///      services.AddMvc();
    ///      services.AddDynamicProxy();
    ///      return services.BuildAspectCoreServiceProvider();
    /// }
    /// 同时设置Repository接口，设置拦截
    ///[RepositoryInterceptor]
    ///public interface IARepository
    /// </summary>
    public class RepositoryInterceptorAttribute : AbstractInterceptorAttribute
    {


        [FromContainer]
        public ILogger<RepositoryInterceptorAttribute> Logger { get; set; }


        [FromContainer]
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                Logger.LogInformation($"{context.Implementation}. { context.ProxyMethod.Name} 开始执行！");
                SetTokenValue(context);
                await next(context);
            }
            catch (Exception exc)
            {
                Logger.LogError($"异常发生：{exc.Message}");
                throw exc;
            }
            finally
            {
                Logger.LogInformation($"{context.Implementation}. { context.ProxyMethod.Name} 执行结束！");
            }
        }


        /// <summary>
        /// 综合设置token值
        /// </summary>
        /// <param name="context"></param>
        private void SetTokenValue(AspectContext context)
        {
            //获取用户名
            var userName = HttpContextAccessor.HttpContext.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(userName))
            {
                //按类父接口RepositoryInterceptorAttribute特性设置值
                foreach (var interfaceItem in context.ImplementationMethod.ReflectedType.GetInterfaces())
                {
                    if (SetTokenValueByType(context, interfaceItem, userName))
                    {
                        return;
                    }
                }
                //按类RepositoryInterceptorAttribute特性设置值
                if (SetTokenValueByType(context, context.ImplementationMethod.ReflectedType, userName))
                {
                    return;
                }
                //按方法RepositoryInterceptorAttribute特性设置值
                if (SetTokenValueByMethod(context, context.ProxyMethod, userName))
                {
                    return;
                }

            }
        }
        /// <summary>
        /// 按类型RepositoryInterceptorAttribute特性设置token值
        /// </summary>
        /// <param name="context">Aspect上下文</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private bool SetTokenValueByType(AspectContext context, Type type, string userName)
        {
            foreach (var atr in type.GetCustomAttributes(false))
            {
                //获取上RepositoryInterceptorAttribute的方法
                if (atr is RepositoryInterceptorAttribute)
                {
                    int index = 0;
                    foreach (var par in context.ProxyMethod.GetParameters())
                    {
                        //参数名为token的切面输入token
                        if (par.Name == "token")
                        {
                            context.Parameters[index] = GetToken(HttpContextAccessor.HttpContext, userName);
                            return true;
                        }
                        index++;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 按方法RepositoryInterceptorAttribute特性设置token值
        /// </summary>
        /// <param name="context">Aspect上下文</param>
        /// <param name="methodInfo">方法</param>
        /// <returns></returns>
        private bool SetTokenValueByMethod(AspectContext context, System.Reflection.MethodInfo methodInfo, string userName)
        {
            foreach (var atr in methodInfo.GetCustomAttributes(false))
            {
                //获取上RepositoryInterceptorAttribute的方法
                if (atr is RepositoryInterceptorAttribute)
                {
                    int index = 0;
                    foreach (var par in context.ProxyMethod.GetParameters())
                    {
                        //参数名为token的切面输入token
                        if (par.Name == "token")
                        {
                            context.Parameters[index] = GetToken(HttpContextAccessor.HttpContext, userName);
                            return true;
                        }
                        index++;
                    }
                }
            }
            return false;
        }

        public string GetToken(HttpContext httpContext, string userName)
        {
            var arr = new byte[1024];
            if (httpContext.Session.TryGetValue(userName, out arr))
            {
                return Encoding.UTF8.GetString(arr).Trim();
            }
            else
            {
                return "";

            }
        }
    }
}
