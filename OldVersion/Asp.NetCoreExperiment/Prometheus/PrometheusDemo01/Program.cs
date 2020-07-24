using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PrometheusDemo01
{
    public static class Program
    {
        public static IMetricsRoot Metrics { get; set; }

        public static IWebHost BuildWebHost(string[] args)
        {
            Metrics = AppMetrics.CreateDefaultBuilder()
                    .OutputMetrics.AsPrometheusPlainText()
                    .OutputMetrics.AsPrometheusProtobuf()
                        .Configuration.Configure(opt => {
                            opt.AddAppTag("app-tag");
                            opt.AddEnvTag("env-tag");
                        })
                    .Build();

            return WebHost.CreateDefaultBuilder(args)
                            .ConfigureMetrics(Metrics)
                            .UseMetrics(
                                options =>
                                {
                                    options.EndpointOptions = endpointsOptions =>
                                    {
                                        endpointsOptions.MetricsTextEndpointOutputFormatter = Metrics.OutputMetricsFormatters.GetType<MetricsPrometheusTextOutputFormatter>();
                                        endpointsOptions.MetricsEndpointOutputFormatter = Metrics.OutputMetricsFormatters.GetType<MetricsPrometheusProtobufOutputFormatter>();
                                    };
                                })
                                .UseUrls("http://*:5000")
                            .UseStartup<Startup>()
                            .Build();
        }

        public static void Main(string[] args) { BuildWebHost(args).Run(); }
    }
  
}