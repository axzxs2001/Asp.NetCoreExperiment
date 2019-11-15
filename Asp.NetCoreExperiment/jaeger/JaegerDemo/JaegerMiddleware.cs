using Microsoft.AspNetCore.Http;
using OpenTracing;
using OpenTracing.Propagation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JaegerDemo
{
    /// <summary>
    /// 请求记录中间件
    /// </summary>
    public class JaegerMiddleware
    {
        private readonly RequestDelegate _next;
        public JaegerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITracer tracer)
        {
            var path = context.Request.Path;
            if (Path.HasExtension(path))
            {
                await _next(context);
            }
            else
            {
                var callingHeaders = new TextMapExtractAdapter(context.Request.Headers.ToDictionary(m => m.Key, m => m.Value.ToString()));
                var spanContex = tracer.Extract(BuiltinFormats.HttpHeaders, callingHeaders);
                ISpanBuilder builder = null;
                if (spanContex != null)
                {
                    builder= tracer.BuildSpan("中间件Span").AsChildOf(spanContex);
                }
                else
                {
                    builder= tracer.BuildSpan("中间件Span");
                }              
                using (IScope scope = builder.StartActive(true))
                {                   
                    scope.Span.SetOperationName(path);
                    // 记录请求信息到span
                    foreach (var query in context.Request.Query)
                    {
                        scope.Span.SetTag(query.Key, query.Value);
                    }
                    if (context.Request.HasFormContentType)
                    {
                        foreach (var form in context.Request.Form)
                        {
                            scope.Span.SetTag(form.Key, form.Value);
                        }
                    }         
                    await _next(context);
                }
            }
        }
   
    }
}
