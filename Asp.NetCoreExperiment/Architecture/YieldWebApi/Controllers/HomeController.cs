using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;
using YieldWebApi.Models;

namespace YieldWebApi.Controllers
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

        [HttpGet("/getents")]
        public async IAsyncEnumerable<Entity> GetEntitys()
        {           
            for (var i = 0; i < 20; i++)
            {
                _logger.LogInformation(i.ToString());
                await Task.Delay(RandomNumberGenerator.GetInt32(100, 500));
                yield return new Entity { ID = i, Time = DateTime.Now };
            }
        }
    }

    public class Entity
    {
        public int ID { get; set; }
        public DateTime Time { get; set; }
    }
}