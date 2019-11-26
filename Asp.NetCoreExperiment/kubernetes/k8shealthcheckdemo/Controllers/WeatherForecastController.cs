using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using Dapper;
namespace k8shealthcheckdemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "this is a ok";
        }
        [HttpGet("/liveness")]
        public  IActionResult Liveness()
        {
            return  Ok();
        }
        [HttpGet("/readiness")]
        public async Task<IActionResult> Readiness()
        {
            var connstring = _configuration.GetConnectionString("postgres");
            using (var con = new NpgsqlConnection(connstring))
            {
                var result = await con.QueryFirstAsync<int>("select id from k8stest limit 1");
                if (result == 1)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500);
                }
            }
        }
    }
}
