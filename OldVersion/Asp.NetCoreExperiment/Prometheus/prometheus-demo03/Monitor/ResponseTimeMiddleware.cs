using Microsoft.AspNetCore.Http;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace prometheus_demo03.Monitor
{
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IMonitoringService service)
        {
            var registry = Metrics.DefaultRegistry;
            if (service.Monitor(context.Request.Method, context.Request.Path))
            {
                var sw = Stopwatch.StartNew();
                await _next(context);
                sw.Stop();

                var histogram =
                    Metrics
                        .WithCustomRegistry(registry)
                         .CreateHistogram("api_response_time_seconds",
                                         "API Response Time in seconds", new HistogramConfiguration()
                                         {
                                             Buckets = new[] { 0.02, 0.05, 0.1, 0.15, 0.2, 0.5, 0.8, 1 },
                                             SuppressInitialValue = true,
                                             LabelNames = new string[] { "method", "path" }
                                         });

                histogram
                    .WithLabels(context.Request.Method, context.Request.Path)
                    .Observe(sw.Elapsed.TotalSeconds);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
