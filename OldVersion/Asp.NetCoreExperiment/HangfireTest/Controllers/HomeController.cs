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
            //简单执行一项任务在开始时，静态方式
            using (var connection = JobStorage.Current.GetConnection())
            {
                var storageConnection = connection as JobStorageConnection;
                if (storageConnection != null)
                {
                    //立即启动
                    var jobId = BackgroundJob.Enqueue(() => TestClass.Once("这是一个参数"));
                }
            }
            return true;
        }
        [HttpPost("onetimes1")]
        public bool FireAndForget1(int? id)
        {
            using (var connection = JobStorage.Current.GetConnection())
            {

                var storageConnection = connection as JobStorageConnection;
                if (storageConnection != null)
                {
                    //简单执行一项任务在开始时，实例化方式
                    //立即启动
                    var jobId = BackgroundJob.Enqueue<TestClass>(x => x.Once1("这是一个参数"));
                }
            }

            return true;
        }
        [HttpPost("dealy")]
        public bool FireAndForget(string id)
        {
            //延迟执行
            BackgroundJob.Schedule(() => TestClass.Dealy("这是一个参数"),
    TimeSpan.FromSeconds(30));
            return true;
        }
        [HttpPost("dealy1")]
        public bool FireAndForget1(string id)
        {
            //延迟执行
            BackgroundJob.Schedule<TestClass>(x => x.Dealy1("这是一个参数"),
    TimeSpan.FromSeconds(30));
            return true;
        }
        [HttpPost("cycle")]
        public bool FireAndForget(double a)
        {
            //周期执行
            RecurringJob.AddOrUpdate(() => TestClass.Cycle("这是一个参数"), "* * * * *", queue: "test");
            return true;
        }
        [HttpPost("cycle1")]
        public bool FireAndForget1(double a)
        {
            //周期执行
            RecurringJob.AddOrUpdate<TestClass>(x => x.Cycle1("这是一个参数"), "* * * * *", queue: "test");
            return true;
        }
    }
}
