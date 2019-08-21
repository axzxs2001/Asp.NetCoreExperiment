using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OAuth2_demo2.Models;

namespace OAuth2_demo2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(new IndexModel
                {
                    GitHubName = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value,
                    GitHubLogin = User.FindFirst(c => c.Type == "urn:github:login")?.Value,
                    GitHubUrl = User.FindFirst(c => c.Type == "urn:github:url")?.Value,
                    GitHubAvatar = User.FindFirst(c => c.Type == "urn:github:avatar")?.Value
                });
            }
            else
            {
                return View();
            }

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
