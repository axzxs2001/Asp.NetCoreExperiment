using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OpenTracing;
using OpenTracing.Propagation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaegerDemo
{
    public class JaegerActionAttribute : TypeFilterAttribute
    {
        public JaegerActionAttribute() : base(typeof(JaegerFilterAttributeImpl))
        {
        }

        private class JaegerFilterAttributeImpl : IActionFilter
        {
            private readonly ITracer _tracer;

            public JaegerFilterAttributeImpl(ITracer tracer)
            {
                _tracer = tracer;
            }
            IScope _scope = null;
            public void OnActionExecuting(ActionExecutingContext context)
            {
                var path = context.HttpContext.Request.Path;
                var callingHeaders = new TextMapExtractAdapter(context.HttpContext.Request.Headers.ToDictionary(m => m.Key, m => m.Value.ToString()));
                var spanContex = _tracer.Extract(BuiltinFormats.HttpHeaders, callingHeaders);
                ISpanBuilder builder = null;
                if (spanContex != null)
                {
                    builder = _tracer.BuildSpan("中间件Span").AsChildOf(spanContex);
                }
                else
                {
                    builder = _tracer.BuildSpan("中间件Span");
                }
                _scope = builder.StartActive(true);
                _scope.Span.SetOperationName(path);
                // 记录请求信息到span
                foreach (var query in context.HttpContext.Request.Query)
                {
                    _scope.Span.SetTag(query.Key, query.Value);
                }
                if (context.HttpContext.Request.HasFormContentType)
                {
                    foreach (var form in context.HttpContext.Request.Form)
                    {
                        _scope.Span.SetTag(form.Key, form.Value);
                    }
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                _scope?.Dispose();
            }
        }



    }
}
