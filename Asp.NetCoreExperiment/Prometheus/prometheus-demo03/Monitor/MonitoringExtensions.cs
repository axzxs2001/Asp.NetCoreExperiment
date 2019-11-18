using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prometheus_demo03.Monitor
{
    public static class MonitoringExtensions
    {
        public static IServiceCollection AddMonitoring(this IServiceCollection services)
        {
            
            return services.AddSingleton<IMonitoringService, MonitoringService>();
        }

        public static IApplicationBuilder UseMonitoring(this IApplicationBuilder builder)
        {
            return builder
                .UseMetricServer()
                .UseMiddleware<ResponseTimeMiddleware>();
        }
    }
}
