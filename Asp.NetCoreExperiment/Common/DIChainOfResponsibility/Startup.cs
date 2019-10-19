using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DIChainOfResponsibility
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
            //职责链依赖注入
            services.AddScoped<EndTask>();
            services.AddScoped<ThirdTask>();
            services.AddScoped<SecondTask>();
            services.AddScoped<FirstTask>();

            //错误姿势
            //services.AddScoped<ITask,EndTask>();
            //services.AddScoped<ITask, ThirdTask>();
            //services.AddScoped<ITask, SecondTask>();
            //services.AddScoped<ITask, FirstTask>();

            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
