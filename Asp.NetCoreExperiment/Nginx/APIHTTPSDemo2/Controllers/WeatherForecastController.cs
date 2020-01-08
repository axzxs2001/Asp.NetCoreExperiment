using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APIHTTPSDemo2.Controllers
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
        [HttpGet]
        public string Get()
        {
            Console.WriteLine($"7443744374437443744374437443744374437443  {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            return "7443的服务端响应";

        }
    }
}
