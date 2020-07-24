using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace CacheDemo02
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

 
        public void ConfigureServices(IServiceCollection services)
        {
            //SQL缓存
            //services.AddDistributedSqlServerCache(options =>
            //{
            //    options.ConnectionString =
            //        @"Data Source=.;Initial Catalog=DistCache;Integrated Security=True;User ID=sa;Password=sa;";
            //    options.SchemaName = "dbo";
            //    options.TableName = "TestCache";
            //});
            //内存缓存
            services.AddDistributedMemoryCache(options =>
            {

            });
            //redis缓存
            //Microsoft.Extensions.Caching.Redis
            //启动docker中的redis命令： docker run -d -p 6379:6379 --name redis01 redis 
            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration = "localhost:6379";              
            //    options.InstanceName = "SampleInstance";
            //});


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
