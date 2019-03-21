using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Health;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PrometheusDemo01
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static string ManageUserUrl = "http://localhost:5000";

        public void ConfigureServices(IServiceCollection services)
        {


            //services.AddMetrics();

            services.AddMetricsReportingHostedService();
            services.AddMetricsTrackingMiddleware();
            services.AddMetricsEndpoints();
            //var hmetrics = AppMetricsHealth.CreateDefaultBuilder().Report.ToMetrics(metrics)
            //    .HealthChecks.AddCheck(new UserServiceHealthCheck("用户接口服务"))
            //    .BuildAndAddTo(services);
            //services.AddHealth(hmetrics);
            //services.AddHealthReportingHostedService();
            //services.AddHealthEndpoints();



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMetricServer();
            app.UseMetricsAllMiddleware();
            app.UseMetricsAllEndpoints();
          //  app.UseHealthAllEndpoints();


            app.UseMvc();
        }
    }
}
