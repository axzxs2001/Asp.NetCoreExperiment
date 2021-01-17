using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimitDemo01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/none")]
        public string None()
        {
            return $"None Ok,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}";
        }
        [HttpGet("/whiteip1")]
        public string WhiteIP1()
        {
            return $"White1 IP Ok,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}";
        }
        [HttpGet("/whiteip2")]
        public string WhiteIP2()
        {
            return $"White2 IP Ok,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}";
        }
        [HttpGet("/clientid")]
        public string ClientID()
        {
            return $"Client ID Ok,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}";
        }
    }
}
