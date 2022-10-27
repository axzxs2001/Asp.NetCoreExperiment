using EChartDemo01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EChartDemo01.Controllers
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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet("/ui")]
        public IActionResult UI()
        {
            var html = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "/a.js",System.Text.Encoding.UTF8);
            return Ok(html);
        }
    }
}