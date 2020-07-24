using LoginProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginProject.Controllers
{
    [Authorize(Roles = "user")]
    public class HomeController : Controller
    {
        readonly ITimeLimitedDataProtector _timeLimitedDataProtector;
        readonly IKeyManager _keyManager;
        readonly IDataProtectionProvider _provider;
        public HomeController(IDataProtectionProvider provider, IKeyManager keyManager)
        {
            _provider = provider;
            _keyManager = keyManager;
            var dataProtector = _provider.CreateProtector("W3E72EFS4MN9LOP0FDWJ7F6E0FSW");
            _timeLimitedDataProtector = dataProtector.ToTimeLimitedDataProtector();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/projects")]
        public IActionResult Projects()
        {
            var user1 = new { UserName = "gsw", Name = "桂素伟", Role = "admin" };
            var code1 = _timeLimitedDataProtector.Protect(Newtonsoft.Json.JsonConvert.SerializeObject(user1), TimeSpan.FromSeconds(10));
            var project1 = new Project { Name = "项目一", Url = $"http://localhost:5000/login?code={code1}", Code = code1 };

            var user2 = new { UserName = "ggg", Name = "谁是谁", Role = "system" };
            var code2 = _timeLimitedDataProtector.Protect(Newtonsoft.Json.JsonConvert.SerializeObject(user2), TimeSpan.FromSeconds(10));
            var project2 = new Project { Name = "项目二", Url = $"http://localhost:5001/login?code={code2}", Code = code2 };
            return new JsonResult(new Project[] { project1, project2 });
        }

        [HttpGet("/overdue")]
        public IActionResult Overdue()
        {
            _keyManager.RevokeAllKeys(DateTimeOffset.Now, "无理由取消");
            return Ok();
        }
        [HttpGet("/read")]
        public IActionResult Read(string code)
        {

            DateTimeOffset dateTimeOffset;
            var result = _timeLimitedDataProtector.Unprotect(code, out dateTimeOffset);
            return Content($"{result}:{dateTimeOffset.LocalDateTime}");
        }
        [AllowAnonymous]
        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == "gsw" && password == "111111")
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Sid, username));
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "桂素伟"));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return Redirect("/");
            }
            if (username == "ggg" && password == "222222")
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Sid, username));
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "谁是谁"));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return Redirect("/");
            }
            else
            {
                return View();
            }
        }
        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/login");
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
    }
    public class Project
    {
        public string Name
        { get; set; }
        public string Url
        { get; set; }

        public string Code
        { get; set; }
    }
}
