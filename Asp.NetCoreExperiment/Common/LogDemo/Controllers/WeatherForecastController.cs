using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LogDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //_logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(new Log { Level = LogEntity.LogLevel.INFO, Message = "测试" }));
            try
            {
                _logger.LogInformation("---------Information------------");
                _logger.LogWarning("----------------warning-------------------");
                _logger.LogError("----------------Error-------------------");
                _logger.LogDebug("----------------warning-------------------");

                _logger.LogTrace("----------------Trace-------------------");
                var k = 0;
                var i = 1 / k;
            }
            catch(Exception exc)
            {
                _logger.LogCritical(exc, "----------------Critical-------------------"+exc.Message);
            }
                var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
