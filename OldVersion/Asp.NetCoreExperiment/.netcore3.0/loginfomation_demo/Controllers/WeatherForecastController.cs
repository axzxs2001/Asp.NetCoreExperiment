using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace loginfomation_demo.Controllers
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
        public IActionResult Get()
        {           
            _logger.LogInformation($"-----------------:{DateTime.Now}");
            return Ok();
        }
    }
}
