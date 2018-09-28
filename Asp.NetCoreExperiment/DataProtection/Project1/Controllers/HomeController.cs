using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Project1.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        readonly ITimeLimitedDataProtector _timeLimitedDataProtector;
        readonly IKeyManager _keyManager;
        public HomeController(IDataProtectionProvider provider, IKeyManager keyManager)
        {
            var dataProtector = provider.CreateProtector("W3E72EFS4MN9LOP0FDWJ7F6E0FSW");
            _timeLimitedDataProtector = dataProtector.ToTimeLimitedDataProtector();
            _keyManager = keyManager;
        }
        [AllowAnonymous]
        [HttpGet("/login")]
        public async Task<IActionResult> Login(string code)
        {
            try
            {
                //单点登录系统中的过期时间
                DateTimeOffset dateTimeOffset;
                var result = _timeLimitedDataProtector.Unprotect(code, out dateTimeOffset);
                var userPro = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(result);

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Sid, userPro.UserName.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, userPro.Role.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, userPro.Name.ToString()));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return Redirect("/");
            }
            catch (CryptographicException)//过期
            {
                return Redirect("/home/error");
            }
        }
        [AllowAnonymous]
        [HttpGet("/overdue")]
        public IActionResult Overdue()
        {
            _keyManager.RevokeAllKeys(DateTimeOffset.Now, "无理由取消");
            return Ok();
        }
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
    }
}
