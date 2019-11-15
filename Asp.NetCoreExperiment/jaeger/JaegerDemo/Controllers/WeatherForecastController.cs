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


        [HttpGet("/test")]
        public string Test()
        {
            return "Test";
        }

    }
}
