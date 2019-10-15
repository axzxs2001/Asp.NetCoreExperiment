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
            services.AddSingleton<ITransfer, EndTransfer>();
            services.AddSingleton<ITransfer, ThirdTransfer>();
            services.AddSingleton<ITransfer, SecondTransfer>();
            services.AddSingleton<ITransfer, FirstTransfer>();
            services.AddSingleton(factory =>
            {
                Func<string, ITransfer> accesor = key =>
                {
                    switch (key)
                    {
                        case "First":
                            return factory.GetService<FirstTransfer>();
                        case "Second":
                            return factory.GetService<SecondTransfer>();
                        case "Third":
                            return factory.GetService<ThirdTransfer>();
                        case "End":
                            return factory.GetService<EndTransfer>();
                        default:
                            throw new ArgumentException($"Not Support key : {key}");
                    }
                };
                return accesor;
            });
        }
    }
}
