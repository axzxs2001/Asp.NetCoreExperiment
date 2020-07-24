using Microsoft.AspNetCore.Http;
using OpenTracing;
using OpenTracing.Propagation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaegerDemo01
{
    /// <summary>
    /// 请求记录中间件
    /// </summary>
    public class JaegerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JaegerOptions _jaegerOptions;
        public JaegerMiddleware(RequestDelegate next, JaegerOptions jaegerOptions = null)
        {
            _jaegerOptions = jaegerOptions;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITracer tracer)
        {

            // Open Tracing
            ISpanBuilder builder = CreateTracingSpanBuilder("中间件Span", tracer, context.Request);
            using (IScope scope = builder.StartActive(true))
            {
                // Span Name
                scope.Span.SetOperationName(context.Request.Path);
                // 记录请求信息到span
                if (_jaegerOptions != null && _jaegerOptions.QuerySpan)
                {
                    foreach (var query in context.Request.Query)
                    {
                        var value = _jaegerOptions.QueryValueMaxLength <= 0 ? query.Value.ToString() : query.Value.ToString().Substring(0, _jaegerOptions.FormValueMaxLength);
                        scope.Span.SetTag(query.Key, query.Value);
                    }
                }
                if (_jaegerOptions != null && _jaegerOptions.FormSpan && context.Request.HasFormContentType)
                {
                    foreach (var form in context.Request.Form)
                    {
                        var value = _jaegerOptions.FormValueMaxLength <= 0 ? form.Value.ToString() : form.Value.ToString().Substring(0, _jaegerOptions.FormValueMaxLength);
                        scope.Span.SetTag(form.Key, value);
                    }
                }
                //foreach (var form in context.Request.Headers)
                //{
                //    scope.Span.SetTag(form.Key, form.Value);
                //}
            }
            await _next(context);

        }
        protected ISpanBuilder CreateTracingSpanBuilder(string spanName, ITracer tracer, HttpRequest request)
        {
            var callingHeaders = new TextMapExtractAdapter(request.Headers.ToDictionary(m => m.Key, m => m.Value.ToString()));
            var spanContex = tracer.Extract(BuiltinFormats.HttpHeaders, callingHeaders);

            if (spanContex != null)
            {
                return tracer.BuildSpan(spanName).AsChildOf(spanContex);
            }
            else
            {
                return tracer.BuildSpan(spanName);
            }
        }
    }
}
