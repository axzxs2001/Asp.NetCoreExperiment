using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Demo001.Models;
using Npgsql;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Demo001.Controllers
{
    //todo 5、防止跨站点请请求伪造xsrf/csrf
    //[ValidateAntiForgeryToken] //应用于所有请求谓词
    [AutoValidateAntiforgeryToken]//不会应用于get,head,options,trace
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        //todo 5、防止跨站点请请求伪造xsrf/csrf
        [HttpPost("/csrf")]
        public string Index([FromBody]Ent ent)
        {
            Console.WriteLine($"ID:{ent.ID}  Name:{ent.Name}");
            return "ok";
        }

        #region 6、上传文件，名字和路径
        //todo  6、上传文件，名字和路径要修改，同时不要放在wwwroot下
        [HttpGet("/files")]
        public IActionResult Files()
        {
            return View();
        }
        [HttpPost("/files")]
        public async Task<IActionResult> Files(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                var filePath = $"{Directory.GetCurrentDirectory()}/uploadfiles/{DateTime.Now.ToString("yyyyMMddHHmmss")}{Path.GetExtension(formFile.FileName)}";
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(new { count = files.Count, size });
        }
        #endregion


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
                    #region 3、防止重定向攻击
                    //todo 3、防止重定向攻击
                    // Redirect不会过滤主机信息，要用LocalRedirect
                    //return Redirect(returnUrl);             
                    return LocalRedirect(returnUrl);
                    #endregion
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
        public async Task<IActionResult> Logout()
        {
            #region 4、logout要使登录的cookie失效
            //todo 4、logout要使登录的cookie失效
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
            #endregion
        }


        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// 2、SQl注放
        /// </summary>
        /// <returns></returns>
        public IActionResult SqlInjection(string p)
        {
            //todo 2、sql注入
            using (var con = new NpgsqlConnection("连接字符串"))
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    #region 2.1
                    //2.1 不正确的
                    //cmd.CommandText = "select * from table1 where id=" + p;
                    //2.1 正确的
                    cmd.CommandText = "select * from table1 where id=@p";
                    cmd.Parameters.AddWithValue("@p", p);
                    #endregion

                    #region 2.2 拼接               
                    var sql2 = new StringBuilder("select * from table1");
                    sql2.Append(" where id=2");
                    cmd.CommandText = sql2.ToString();
                    cmd.Parameters.AddWithValue("@p", p);
                    #endregion

                    #region 2.3 拼接非字符串数据
                    var sql3 = new StringBuilder("select * from table1");
                    sql3.Append(" where id=" + 2);
                    cmd.CommandText = sql3.ToString();
                    #endregion

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public class Ent
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
