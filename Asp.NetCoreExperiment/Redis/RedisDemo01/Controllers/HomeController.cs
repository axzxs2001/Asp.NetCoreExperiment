using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisDemo01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;

        private readonly IDatabase _db;

        public HomeController(ILogger<HomeController> logger, IConnectionMultiplexer multiplexer)
        {
            _db = multiplexer.GetDatabase();
            _logger = logger;
        }

        [HttpGet("/write")]
        public async Task<string> Write()
        {
            var result = await _db.StringSetAsync("time", DateTime.Now.ToString());
            return result.ToString();
        }

        [HttpGet("/read")]
        public async Task<string> Read()
        {
            var result = await _db.StringGetAsync("time");
            return result.ToString();
        }
    }
}
