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

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }
        private readonly IHttpClientFactory _clientFactory;


        [HttpGet]
        public async Task<string> Get()
        {

            //GlobalTracer.Instance.ActiveSpan.SetTag("TracingSpanTag.GatewayResponse", "桂素伟");


            // Open Tracing
            ISpanBuilder builder = CreateTracingSpanBuilder(Startup.tracer, Request);
            using (IScope scope = builder.StartActive(true))
            {
                // Span Name
                scope.Span.SetOperationName("JaegerDemo/WeatherForecastController/get");

                // 记录请求信息到span
                scope.Span.SetTag("TracingSpanTag.ApiRequest", "桂素伟00000000000000");

                var request = new HttpRequestMessage(HttpMethod.Get, "/demo01");
                var client = _clientFactory.CreateClient("nameclient5000");               
                var response = await client.SendAsync(request);          
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            return "none";
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
