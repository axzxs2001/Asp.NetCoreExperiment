using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            //Thread.Sleep(1000);
            TakeCPU().Wait();
            sw.Stop();
            this._logger.LogInformation($"{sw.ElapsedMilliseconds }ms, thread count:{ThreadPool.ThreadCount}");
            return "sync";
        }

        [HttpGet("/async")]
        public async Task<string> Async()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await TakeCPU();
            // await Task.Delay(1000);
            sw.Stop();
            this._logger.LogInformation($"{sw.ElapsedMilliseconds }ms, thread count:{ThreadPool.ThreadCount}");
            return "async";
        }

        public async Task TakeCPU()
        {
            var str = "Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.";
            for (var i = 0; i < 100000; i++)
            {
                str = await MD5Hash(str);
            }
        }
        public async Task<string> MD5Hash(string input)
        {
            var hash = new StringBuilder();
            var md5provider = new MD5CryptoServiceProvider();
            var bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return await Task.FromResult<string>(hash.ToString());
        }
    }
}
