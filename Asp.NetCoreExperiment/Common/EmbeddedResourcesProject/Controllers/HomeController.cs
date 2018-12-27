using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmbeddedResourcesProject.Models;
using System.Reflection;

namespace EmbeddedResourcesProject.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/home")]
        public IActionResult Index()
        {
            var ss = Assembly.GetExecutingAssembly().GetManifestResourceNames();

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
    }
}
