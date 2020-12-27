using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Propagation;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JaegerSharp
{
    /// <summary>
    /// Jaeger中间件
    /// </summary>
    public class JaegerSharpMiddleware
    {
        /// <summary>
        /// jaeger选项
        /// </summary>
        private readonly JaegerOptions _jaegerOptions;


        private readonly ILogger<JaegerSharpMiddleware> _logger;
        /// <summary>
        /// 请求代理
        /// </summary>
        private readonly RequestDelegate _next;
        public JaegerSharpMiddleware(RequestDelegate next, ILogger<JaegerSharpMiddleware> logger, JaegerOptions jaegerOptions = null)
        {
            _next = next;
            _jaegerOptions = jaegerOptions;
            _logger = logger;

        }
        /// <summary>
        /// 调用管道
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="tracer">跟踪器</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, ITracer tracer)
        {
            _logger.LogInformation("jaeger调用");
            var path = context.Request.Path;
            if (Path.HasExtension(path))
            {
                await _next(context);
            }
            else
            {
                //接收传入的Headers
                var callingHeaders = new TextMapExtractAdapter(context.Request.Headers.ToDictionary(m => m.Key, m => m.Value.ToString()));
                var spanContex = tracer.Extract(BuiltinFormats.HttpHeaders, callingHeaders);
                ISpanBuilder builder = null;
                if (spanContex != null)
                {
                    builder = tracer.BuildSpan("中间件Span").AsChildOf(spanContex);
                }
                else
                {
                    builder = tracer.BuildSpan("中间件Span");
                }
                //开始设置Span
                using (IScope scope = builder.StartActive(true))
                {
                    scope.Span.SetOperationName(path);             
                    // 记录请求信息到span
                    if (_jaegerOptions.IsQuerySpan)
                    {
                        foreach (var query in context.Request.Query)
                        {
                            //包含敏感词跳出
                            if (_jaegerOptions.NoSpanKeys.Contains(query.Key))
                            {
                                continue;
                            }
                            var value = query.Value.ToString().Length > _jaegerOptions.QueryValueMaxLength ? query.Value.ToString()?.Substring(0, _jaegerOptions.QueryValueMaxLength) : query.Value.ToString();
                            scope.Span.SetTag(query.Key, value);
                        }
                    }
                    if (_jaegerOptions.IsFormSpan && context.Request.HasFormContentType)
                    {
                        foreach (var form in context.Request.Form)
                        {
                            //包含敏感词跳出
                            if (_jaegerOptions.NoSpanKeys.Contains(form.Key))
                            {
                                continue;
                            }
                            var value = form.Value.ToString().Length > _jaegerOptions.FormValueMaxLength ? form.Value.ToString()?.Substring(0, _jaegerOptions.FormValueMaxLength) : form.Value.ToString();
                            scope.Span.SetTag(form.Key, value);
                        }
                    }
                    await _next(context);
                }
            }
        }
    }
}
