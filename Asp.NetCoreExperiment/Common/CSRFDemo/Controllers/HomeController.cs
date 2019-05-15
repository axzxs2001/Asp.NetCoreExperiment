using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSRFDemo.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace CSRFDemo.Controllers
{
    [Authorize(Roles = "admin,system")]
    //[ValidateAntiForgeryToken] //应用于所有请求谓词
    [AutoValidateAntiforgeryToken]//不会应用于get,head,options,trace
    public class HomeController : Controller
    {
 
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string v1)
        {
            Console.WriteLine("------------------"+v1);
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

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }
        [IgnoreAntiforgeryToken]//忽略这个提交
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(string userName, string password, string returnUrl = null)
        {
            var list = new List<dynamic> {
                 new { UserName = "gsw", Password = "111111", Role = "admin",Name="桂素伟" },
                 new { UserName = "aaa", Password = "222222", Role = "system" ,Name="路人甲"}
             };
            var user = list.SingleOrDefault(s => s.UserName == userName && s.Password == password);
            if (user != null)
            {
                //用户标识
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Sid, userName));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                if (returnUrl == null)
                {
                    returnUrl = TempData["returnUrl"]?.ToString();
                }
                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }
            else
            {
                return BadRequest("用户名或密码错误！");
            }
        }
    }
}
