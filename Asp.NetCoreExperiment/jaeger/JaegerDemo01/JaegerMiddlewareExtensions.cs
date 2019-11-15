using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaegerDemo01
{
    /// <summary>
    /// 扩展中间件
    /// </summary>
    public static class JaegerMiddlewareExtensions
    {
        public static IApplicationBuilder UseJager(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JaegerMiddleware>();
        }

    }
}
