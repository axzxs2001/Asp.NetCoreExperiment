using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo01.Services;

namespace WebDemo01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        private readonly IReadDapper _read;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IReadDapper read)
        {
            _logger = logger;
            _read = read;
        }

        [HttpGet]
        public string Get()
        {
          return  "OK";
        }
    }
}
