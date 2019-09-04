using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransitEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PublishDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController(IBusControl bus, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _bus = bus;
        }

        private readonly IBusControl _bus;
        private readonly ILogger<WeatherForecastController> _logger;

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation($"发送：gsw,523");
            var time = DateTime.Now;
            _bus.Publish(new Class1 { Name = "gsw", Age = 523, CreateTime = time });
            return $"发送：gsw,523,{time}";
        }
    }
}
