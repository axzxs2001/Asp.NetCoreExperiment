using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using webmvcdemo002.Models;

namespace webmvcdemo002.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IFeatureManager _featureManager;

        public HomeController(ILogger<HomeController> logger, IFeatureManager featureManager)
        {
            _featureManager = featureManager;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            if (await _featureManager.IsEnabledAsync(nameof(MyFeatureFlags.Privacy)))
            {
                Console.WriteLine("Privacy 可用");
            }
            else
            {
                Console.WriteLine("Privacy 不可用");
            }
            if (await _featureManager.IsEnabledAsync(nameof(MyFeatureFlags.PublicPage)))
            {
                Console.WriteLine("PublicPage 可用");
            }
            else
            {
                Console.WriteLine("PublicPage 不可用");
            }
            return View();
        }

        [FeatureGate(MyFeatureFlags.Privacy)]
        public IActionResult Privacy()
        {
            return View();
        }

        [FeatureGate(MyFeatureFlags.PublicPage)]
        public IActionResult PublicPage()
        {
            return View();
        }
        [FeatureGate(MyFeatureFlags.RadomPage)]
        public IActionResult RadomPage()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public enum MyFeatureFlags
    {
        Privacy,
        PublicPage,
        RadomPage
    }
}
