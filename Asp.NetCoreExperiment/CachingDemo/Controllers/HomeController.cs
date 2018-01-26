using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CachingDemo.Models;
using EasyCaching.Core;

namespace CachingDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEasyCachingProvider _provider;
        private readonly IDateTimeService _service;
        public HomeController(IEasyCachingProvider provider, IDateTimeService service)
        {
            this._provider = provider;
            this._service = service;
        }
        public IActionResult Index()
        {
            _provider.Set("demo", "这是一个测试的缓存Demo", TimeSpan.FromMinutes(1));
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page." + _provider.Get<string>("demo"); ;

            return View();
        }

        public IActionResult Contact(string name)
        {
            ViewData["Message"] = "Your contact page." +  _service.GetCurrentUtcTime(name); 

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
