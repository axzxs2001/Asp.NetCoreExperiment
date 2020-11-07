using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using WebError.Models;

namespace WebError.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var ran = new Random();
            switch (ran.Next(1, 4))
            {
                case 1:
                    int i = 0;
                    var j = 10 / i;
                    return Ok();
                case 2:
                    throw new RegisteredException("这是一个错误");
                default:
                    return View();
            }
        }
 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();       
            //如果是业务自定义异常，进行特殊处理
            if (context.Error is DaMeiException)
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = context.Error.Message, ErrorType = "His" });

            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = context.Error.Message, ErrorType = "System" });
            }
        }
    }
}
