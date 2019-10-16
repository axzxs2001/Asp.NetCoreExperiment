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
            services.AddChainOfResponsibility();
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
    static class ChainOfResponsibilityExtension
    {
        public static void AddChainOfResponsibility(this IServiceCollection services)
        {
            services.AddSingleton<EndTransfer>();
            services.AddSingleton<ThirdTransfer>();
            services.AddSingleton<SecondTransfer>();
            services.AddSingleton<FirstTransfer>();
        }
    }
}
