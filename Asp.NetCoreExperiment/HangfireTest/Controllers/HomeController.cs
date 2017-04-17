using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Hangfire.Storage;
using Microsoft.Extensions.Logging;
using HangfireTest.Model.Repository;
using HangfireTest.Model;

namespace HangfireTest.Controllers
{
    public class HomeController : Controller
    {


        /// <summary>
        /// 日志对象
        /// </summary>
        ILogger _log;
        public HomeController(ILoggerFactory loggerFactory)
        {
            _log = loggerFactory.CreateLogger<HomeController>();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        [HttpGet("fireandforget")]
        public IActionResult FireAndForget()
        {
            return View();
        }
        [HttpPost("onetimes")]
        public bool FireAndForget(int? id)
        {
            //简单执行一项任务在开始时
            using (var connection = JobStorage.Current.GetConnection())
            {
                var storageConnection = connection as JobStorageConnection;
                if (storageConnection != null)
                {
                    //立即启动
                    var jobId = BackgroundJob.Enqueue(() => TestClass.FFF());
                    //loggerFactory.CreateLogger("aaa").LogInformation($"JobID:{jobId}");
                }
            }
            return true;
        }

        [HttpPost("dealy")]
        public bool FireAndForget(string id)
        {
            BackgroundJob.Schedule(
    () => Console.WriteLine("延时===================="),
    TimeSpan.FromSeconds(30));
            return true;
        }

    }
}
