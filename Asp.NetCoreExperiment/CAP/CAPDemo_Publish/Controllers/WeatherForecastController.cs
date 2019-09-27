using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CAPDemo_Publish.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICapPublisher _capBus;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICapPublisher capPublisher)
        {
            _capBus = capPublisher;
            _logger = logger;
        }

        [HttpGet]
        public bool Get()
        {
            _capBus.Publish("xxx.services.show.time", System.Text.Json.JsonSerializer.Serialize(new {ID=1,Value="aaaaaaa" }));
            return true;
        }
    }
}
