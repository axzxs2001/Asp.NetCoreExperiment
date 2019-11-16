using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prometheus;
using Prometheus.Client.AspNetCore;

namespace prometheus_demo01
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
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        
            // app.UsePrometheusServer();
            #region Prometheus.Net
            //var counter = Metrics.CreateCounter("PathCounter", "Counts requests to endpoints", new CounterConfiguration
            //{
            //    LabelNames = new[] { "method", "endpoint" }
            //});
            //app.Use((context, next) =>
            //{
            //    counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
            //    return next();
            //});
            //app.UseMetricServer();
            //app.UseHttpMetrics();
            //app.UseHttpMetrics(options =>
            //{
            //    options.RequestCount.Enabled = false;

            //    options.RequestDuration.Histogram = Metrics.CreateHistogram("myapp_http_request_duration_seconds", "Some help text",
            //        new HistogramConfiguration
            //        {
            //            Buckets = Histogram.LinearBuckets(start: 1, width: 1, count: 64),
            //            LabelNames = new[] { "code", "method" }
            //        });
            //});
            #endregion
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UsePrometheusServer();
            app.UseHttpMetrics();

            app.UseMiddleware<ResponseTimeMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
   
            //app.UsePrometheusServer(q =>
            //{
            //    q.CollectorRegistryInstance = new CollectorRegistry();
            //    q.MapPath = "/default-metrics";
            //});
        }
    }
}
