using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AOPDemo.Models;
using AOPDemo.Models.Repository;

namespace AOPDemo.Controllers
{
    public class HomeController : Controller
    {
        IItemManageRepository _imteManageRepository;
        public HomeController(IItemManageRepository imteManageRepository)
        {
            _imteManageRepository = imteManageRepository;
        }
        public IActionResult Index()
        {
            _imteManageRepository.AddItem(null);
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
