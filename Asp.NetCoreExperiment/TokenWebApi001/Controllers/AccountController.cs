using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TokenWebApi001.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    public class AccountController : Controller
    {
        [HttpPost]
        public JsonResult ABC()
        {
            return new JsonResult(new
            {
                Name = "张三",
                Age = 12,
                Sex = true,
                User = User.Identity.Name,


            }, new Newtonsoft.Json.JsonSerializerSettings());
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登录action
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="role">角色</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password, string role)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKeyForAspNetCoreAPIToken"));
            var options = new TokenProviderOptions
            {
                Audience = "audience",
                Issuer = "issuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            };
            var tpm = new TokenProvider(options);
            var token = await tpm.GenerateToken(HttpContext, username, password, role);
            if (null != token)
            {
                return new JsonResult(token);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
