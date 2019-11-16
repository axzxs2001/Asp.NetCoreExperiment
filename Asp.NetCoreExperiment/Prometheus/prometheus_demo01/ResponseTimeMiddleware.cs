using Microsoft.AspNetCore.Http;
using Prometheus.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace prometheus_demo01
{
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            await _next(context);
            sw.Stop();

            var histogram =
                Metrics
                    .CreateHistogram(
                        "api_response_time_seconds",
                        "API Response Time in seconds",
                        new[] { 0.02, 0.05, 0.1, 0.15, 0.2, 0.5, 0.8, 1 },
                        "method",
                        "path");

            histogram
                .WithLabels(context.Request.Method, context.Request.Path)
                .Observe(sw.Elapsed.TotalSeconds);
        }
    }
}
