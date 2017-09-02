using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PolicyPrivilegeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace PolicyPrivilegeManagement.Controllers
{
    [Authorize(Policy = "Permission")]
    //[Authorize(Policy = "RequireClaim")]
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

        [HttpPost("addpermission")]
        public IActionResult AddPermission(string url,string userName)
        {
            //添加权限
            PermissionRequirement.UserPermissions.Add(new UserPermission { Url = url, UserName = userName });
            return Content("添加成功");
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
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
            var list = new List<dynamic> {
                new { UserName = "gsw", Password = "111111", Role = "admin",Name="桂素伟",Country="中国",Date="2017-09-02",BirthDay="1979-06-22"},
                new { UserName = "aaa", Password = "222222", Role = "system",Name="测试A" ,Country="美国",Date="2017-09-03",BirthDay="1999-06-22"}
            };
            var user = list.SingleOrDefault(s => s.UserName == userName && s.Password == password);
            if (user != null)
            {
                //用户标识
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Sid, userName));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
                identity.AddClaim(new Claim(ClaimTypes.Country, user.Country));
                identity.AddClaim(new Claim("date", user.Date));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                if (returnUrl == null)
                {
                    returnUrl = TempData["returnUrl"]?.ToString();
                }
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else
            {
                const string badUserNameOrPasswordMessage = "用户名或密码错误！";
                return BadRequest(badUserNameOrPasswordMessage);
            }
        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }
    }
}
