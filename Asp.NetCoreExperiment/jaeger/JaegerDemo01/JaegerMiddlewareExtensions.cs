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
        public static IApplicationBuilder UseJager(this IApplicationBuilder builder, JaegerOptions jaegerOptions)
        {
            return builder.UseMiddleware<JaegerMiddleware>(jaegerOptions);
        }

    }

    public class JaegerOptions
    {
        public bool FormSpan { get; set; }
        public int FormValueMaxLength { get; set; }
        public bool QuerySpan { get; set; }
        public int QueryValueMaxLength { get; set; }

    }
}
