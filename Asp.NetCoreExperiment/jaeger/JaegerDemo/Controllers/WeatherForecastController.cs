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
        private readonly IHttpClientFactory _clientFactory;
    

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory clientFactory, ITracer tracer)
        {
            _logger = logger;
               _tracer = tracer;
            _clientFactory = clientFactory;
        }

        [JaegerAction]
        [HttpGet]
        public async Task<string> Get()
        {
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
        }

        [JaegerAction]
        [HttpGet("/test")]
        public string Test()
        {
            return "test";
        }

    }
}
