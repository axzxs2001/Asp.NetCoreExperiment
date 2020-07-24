using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core.Internal;
using EasyCaching.InMemory;
using EasyCaching.Interceptor.Castle;
using EasyCaching.Memcached;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CachingDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();

            services.AddMvc();
            services.AddDefaultInMemoryCache();
            return services.ConfigureCastleInterceptor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
           // app.UseDefaultMemcached();
        }
    }

    public interface IDateTimeService
    {
        string GetCurrentUtcTime(string name);
    }

    public class DateTimeService : IDateTimeService, IEasyCaching
    {
        /// <summary>
        /// 参数不同，缓存结果也不相同
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [EasyCachingInterceptor(Expiration = 10)]
        public string GetCurrentUtcTime(string name)
        {
            return System.DateTime.UtcNow.ToString()+"--------"+name;
        }
    }
}
