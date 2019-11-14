using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Propagation;

namespace JaegerDemo01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
     

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/demo01")]
        public string Demo01()
        {
            // Open Tracing
            ISpanBuilder builder = CreateTracingSpanBuilder(Startup.tracer, Request);
            using (IScope scope = builder.StartActive(true))
            {
                // Span Name
                scope.Span.SetOperationName("JaegerDemo01/WeatherForecastController/get");

                // 记录请求信息到span
                scope.Span.SetTag("TracingSpanTag.ApiRequest", "桂素伟1111111111111111111");
            }
            return "桂素伟1111111111111111111";
        }

        protected ISpanBuilder CreateTracingSpanBuilder(ITracer tracer, HttpRequest request)
        {
            var callingHeaders = new TextMapExtractAdapter(request.Headers.ToDictionary(m => m.Key, m => m.Value.ToString()));
            ISpanContext spanContex = tracer.Extract(BuiltinFormats.HttpHeaders, callingHeaders);
            ISpanBuilder builder;
            if (spanContex != null)
                builder = tracer.BuildSpan("").AsChildOf(spanContex);
            else
                builder = tracer.BuildSpan("");
            return builder;
        }
    }
}
