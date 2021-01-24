using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Web002.Models;

namespace AuthenticationAuthorization_Cookie_01.Controllers
{
    [Authorize(Policy = "Permission")]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }
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

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }
        [HttpGet("adminpage")]
        public IActionResult AdminPage()
        {
            return View();
        }
        [HttpGet("systempage")]
        public IActionResult SystemPage()
        {
            return View();
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("/photo/{id}")]
        public IActionResult GetPohot(int id)
        {
            //数据集全
            var photos = new List<dynamic>
            {
                new {ID=1, Name="第一张",User="A"},
                new {ID=2, Name="第二张",User="B"},
                new {ID=3, Name="第三张",User="A"},
                new {ID=4, Name="第四张",User="B"}
            };
            //越权
            //return new JsonResult(photos.SingleOrDefault(s => s.ID == id));
            //增加数据所属条件
            return new JsonResult(photos.SingleOrDefault(s => s.ID == id && s.User == User.Identity.Name));
        }
    }
}
