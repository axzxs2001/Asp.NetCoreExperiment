using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLog_JsonLog.Controllers
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

            _logger.LogTrace("values.Get方法-LogTrace跟踪");
            _logger.LogDebug("values.Get方法-LogDebug调试");
            _logger.LogInformation("values.Get方法-LogInformation信息 ");
            _logger.LogWarning("values.Get方法-LogWarning警告 ");
            _logger.LogError("values.Get方法-LogError错误 ");
            var exce = new Exception("aaaaaaaaa\r\nbbbbbbbbbbb\r\ncccccc\"cccc");
            _logger.LogCritical(exce, "values.Get方法-LogCritical严重 " + exce.Message);

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
