using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppMetricsDemo01.Models;

namespace AppMetricsDemo01.Controllers
{
    public class HomeController : Controller
    {
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet("/health")]
        public IActionResult Health()
        {
            return Ok();
        }


        [HttpPost("/abc")]
        public IActionResult PostAAA(string id)
        {
            return Redirect("/home/Contact");
        }
        [HttpPut("/abcd")]
        public IActionResult PutAAA(string id)
        {
            return Redirect("/home/Contact");
        }
    }
}
