using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrometheusSample.Middlewares
{
    /// <summary>
    /// 扩展中间件
    /// </summary>
    public static class BusinessMetricsMiddlewareExtensions
    {
        public static IApplicationBuilder UseBusinessMetrics(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BusinessMetricsMiddleware>();
        }
    }
}
