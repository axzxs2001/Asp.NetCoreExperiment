using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Asp.NetCore_WebPage.Model.Repository;
using System.Security.Claims;

namespace Asp.NetCore_WebPage.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            return View();
            //return Redirect("/");
        }
        [HttpGet("error")]
        public IActionResult Error()
        {
            return View();
        }
        /// <summary>
        /// 无权限
        /// </summary>
        /// <returns></returns>
        [HttpGet("nopermission")]
        public IActionResult NoPermission()
        {
            return View();
        }
    }
}
