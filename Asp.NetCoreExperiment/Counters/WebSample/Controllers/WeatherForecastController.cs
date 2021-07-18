using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebSample.Controllers
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
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var client = new HttpClient();
            var content = await client.GetStringAsync("https://www.google.com");

            var rng = new Random();
            var _list = Enumerable.Range(1, 10).Select(index => new WeatherForecast
            {
                Num = accumulate(),
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Describe = ToMD5Hash(Summaries[rng.Next(Summaries.Length)]) + content,
            })
            .ToArray();
            return _list;
        }

        ulong accumulate()
        {
            ulong num = 0;
            for (ulong i = 1; i <= 10; i++)
            {
                num = i;
            }
            return num;
        }
        string ToMD5Hash(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            var bytes = Encoding.ASCII.GetBytes(str);
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }
            using (var md5 = MD5.Create())
            {
                return string.Join("", md5.ComputeHash(bytes).Select(x => x.ToString("X2")));
            }
        }
    }
}
