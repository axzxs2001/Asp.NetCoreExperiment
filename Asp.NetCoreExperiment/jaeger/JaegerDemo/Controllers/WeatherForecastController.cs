using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Propagation;
using OpenTracing.Util;

namespace JaegerDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITracer _tracer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory clientFactory, ITracer tracer)
        {
            _logger = logger;
            _tracer = tracer;
            _clientFactory = clientFactory;
        }
        private readonly IHttpClientFactory _clientFactory;


        [HttpGet]
        public async Task<string> Get()
        {
            //ISpanBuilder builder = CreateTracingSpanBuilder(Startup.tracer, Request);
            //using (IScope scope = builder.StartActive(true))
            //{
            //    GlobalTracer.Instance.ActiveSpan.SetTag("TracingSpanTag.GatewayResponse", "桂素伟");
            //}

            // Open Tracing

            var request = new HttpRequestMessage(HttpMethod.Get, "/demo01");
            var client = _clientFactory.CreateClient("nameclient5000");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                return "";
            }

            //return await SpanTag("获取Demo1数据", ("name", "桂素伟"), async () =>
            // {
            //     //UI  http://127.0.0.1:16686/
            //     var request = new HttpRequestMessage(HttpMethod.Get, "/demo01");
            //     var client = _clientFactory.CreateClient("nameclient5000");
            //     var response = await client.SendAsync(request);
            //     if (response.IsSuccessStatusCode)
            //     {
            //         var result = await response.Content.ReadAsStringAsync();
            //         return result;
            //     }
            //     else
            //     {
            //         return "";
            //     }
            // });
        }

        async Task<string> SpanTag(string operationName, (string key, string value) tagPair, Func<Task<string>> func)
        {
            ISpanBuilder builder = CreateTracingSpanBuilder(_tracer, Request);
            using (IScope scope = builder.StartActive(true))
            {
                // Span Name
                scope.Span.SetOperationName(operationName);
                // 记录请求信息到span
                scope.Span.SetTag(tagPair.key, tagPair.value);
                return await func();
            }
        }



        protected ISpanBuilder CreateTracingSpanBuilder(ITracer tracer, HttpRequest request)
        {
            var callingHeaders = new TextMapExtractAdapter(request.Headers.ToDictionary(m => m.Key, m => m.Value.ToString()));
            var spanContex = tracer.Extract(BuiltinFormats.HttpHeaders, callingHeaders);

            if (spanContex != null)
            {
                return tracer.BuildSpan("").AsChildOf(spanContex);
            }
            else
            {
                return tracer.BuildSpan("");
            }
        }


    }
}
