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
    public class Program
    {
        public static IMetricsRoot Metrics { get; set; }

        public static IWebHost BuildWebHost(string[] args)
        {
            Metrics = AppMetrics.CreateDefaultBuilder()
                .OutputMetrics.AsPrometheusPlainText()
                .OutputMetrics.AsPrometheusProtobuf()
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .ConfigureMetrics(Metrics)
                .UseMetrics(
                    options =>
                    {
                        options.EndpointOptions = endpointsOptions =>
                        {
                            foreach (var formatter in Metrics.OutputMetricsFormatters)
                            {
                                switch (formatter)
                                {
                                    case IMetricsOutputFormatter newformatter when newformatter is MetricsPrometheusTextOutputFormatter:
                                        endpointsOptions.MetricsTextEndpointOutputFormatter = formatter;
                                        break;
                                    case IMetricsOutputFormatter newformatter when newformatter is MetricsPrometheusProtobufOutputFormatter:
                                        endpointsOptions.MetricsTextEndpointOutputFormatter = formatter;
                                        break;
                                }
                            }                     
                        };
                    })
                .UseKestrel(options => options.Listen(IPAddress.Any, 5000))
                .UseStartup<Startup>()
                .Build();
        }

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
    }
}
