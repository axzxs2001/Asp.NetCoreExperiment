using AOPDemo.Models.Repository;
using AspectCore.DynamicProxy;
using AspectCore.Injector;
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

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {               
                Logger.LogInformation($"{context.Implementation}. { context.ProxyMethod.Name} 开始执行！");           
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
    }
}
