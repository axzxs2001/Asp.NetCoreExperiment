using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MultiChildDI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiChildDI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        //private readonly IDemoServiceFactory _factory;

        //public HomeController(ILogger<HomeController> logger, IDemoServiceFactory factory)
        //{
        //    _factory = factory;
        //    _logger = logger;
        //}
        //[HttpGet]
        //public string Get(string name)
        //{
        //    var demo = _factory.Create(name);
        //    demo.F1();
        //    //var demo01S = _provider.GetService(typeof(DemoService01));
        //    //var demo01 = demo01S as DemoService01;
        //    //demo01.F1();

        //    //var demo02 = _provider.GetService<DemoService02>();
        //    //demo02.F1();

        //    //var demo03 = _provider.GetService<DemoService03>();
        //    //demo03.F1();

        //_services.First().F1();
        //    return "ok";
        //}


        private readonly IEnumerable<IDemoService> _services;
        public HomeController(ILogger<HomeController> logger, IEnumerable<IDemoService> services)
        {
            _services = services;
            _logger = logger;
        }
        [HttpGet("/first")]
        public string First()
        {
            _logger.LogInformation("first");
            var demo01 = _services.SingleOrDefault(s => s.GetType().Name == "DemoService01");
            demo01.F1();
            return "ok";
        }
        [HttpGet("/seconderror")]
        public string SecondError([FromServices] IServiceProvider provider)
        {
            var demo01 = provider.GetService<DemoService01>();
            demo01.F1();
            return "ok";
        }
        [HttpGet("/second")]
        public string Second([FromServices] IServiceProvider provider)
        {
            _logger.LogInformation("second");
            var demo01 = provider.GetServices<IDemoService>().First();
            demo01.F1();
            return "ok";
        }
    }
}
