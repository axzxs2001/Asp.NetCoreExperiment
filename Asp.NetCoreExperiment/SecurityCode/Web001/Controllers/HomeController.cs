using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web001.Models;
using Web001.Services;

namespace Web001.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(string userName, string password, string returnUrl = "/")
        {
            //这里省略n行代码
            //return Redirect(returnUrl);

            return LocalRedirect(returnUrl);
            //or
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("/error");
            }
        }
        public async Task<IActionResult> Logout()
        {
            //logout要使登录的cookie失效
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");

        }
        //[AllowAnonymous]
        //[HttpPost("login")]
        //public IActionResult Login(string userName, string password, string returnUrl = null)
        //{
        //    //防止重定向攻击,Redirect不会过滤主机信息，要用LocalRedirect
        //    //return Redirect(returnUrl);             
        //    return LocalRedirect(returnUrl);
        //}
        [HttpPost("/files")]
        public async Task<IActionResult> Files(List<IFormFile> files)
        {
            //要据上传文件的特征，一定要验证用户上传文件的可信度
            var size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                //扩展名
                var extension = Path.GetExtension(formFile.FileName);
                var filePath = $"{Directory.GetCurrentDirectory()}/uploadfiles/{DateTime.Now.ToString("yyyyMMddHHmmss")}{extension}";
                if (formFile.Length > 0)
                {
                    var extesion = Path.GetExtension(formFile.FileName);
                    var stream = new FileStream(path: filePath, mode: FileMode.Create);

                    await formFile.CopyToAsync(stream);
                }
            }

            return Ok(new { count = files.Count, size });
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


        public async Task<IActionResult> GetUsers(string name)
        {
            _logger.LogInformation("SQL注入");
            var list = await _userService.GetUsersAsync(name);
            return new JsonResult(list.SingleOrDefault());
        }
    }
}
