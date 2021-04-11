using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotRealoadDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private static string _dateTimeStr;
        public HomeController(ILogger<HomeController> logger)
        {
            if (HomeController._dateTimeStr == null)
            {
                _dateTimeStr = DateTime.Now.ToString();
            }
            _logger = logger;
        }

        [HttpGet("/state")]
        public string GetStastus()
        {
            _logger.LogInformation("获取时间列表");
            return "当前存储时间是：" + _dateTimeStr;
        }

        [HttpGet("/sleep")]
        public string Sleep(int i)
        {
            _logger.LogInformation("延时");
            if (i == 1)
            {
                System.Threading.Thread.Sleep(10000);
            }
            return "延时结果gsw";
        }
    }
}
