using Microsoft.AspNetCore.Builder;

namespace JaegerSharp
{
    /// <summary>
    /// 使用JaegerSharp中间件
    /// </summary>
    public static class JaegerSharpMiddlewareExtensions
    {
        public static IApplicationBuilder UseJaegerSharp(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JaegerSharpMiddleware>(new JaegerOptions());
        }
        public static IApplicationBuilder UseJaegerSharp(this IApplicationBuilder builder, JaegerOptions jaegerOptions)
        {
            return builder.UseMiddleware<JaegerSharpMiddleware>(jaegerOptions);
        }
    }
}
