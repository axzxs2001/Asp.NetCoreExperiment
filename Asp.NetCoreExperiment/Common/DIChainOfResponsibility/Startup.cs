using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddChainOfResponsibility();
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
    static class ChainOfResponsibilityExtension
    {
        public static void AddChainOfResponsibility(this IServiceCollection services)
        {
            services.AddTransient(typeof(EndTransfer));
            services.AddTransient(typeof(ThirdTransfer));
            services.AddTransient(typeof(SecondTransfer));
            services.AddTransient<ParentTransfer, FirstTransfer>();     
        }
    }
}
