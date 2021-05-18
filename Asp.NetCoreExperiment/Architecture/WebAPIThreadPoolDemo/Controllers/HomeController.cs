using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPIThreadPoolDemo.Controllers
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

        [HttpGet("/sync")]
        public string Sync()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Thread.Sleep(1000);
            sw.Stop();
            this._logger.LogInformation($"{sw.ElapsedMilliseconds }ms, thread count:{ThreadPool.ThreadCount}");
            return "sync";
        }

        [HttpGet("/async")]
        public async Task<string> Async()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await Task.Delay(1000);
            sw.Stop();
            this._logger.LogInformation($"{sw.ElapsedMilliseconds }ms, thread count:{ThreadPool.ThreadCount}");
            return "async";
        }
    }
}
