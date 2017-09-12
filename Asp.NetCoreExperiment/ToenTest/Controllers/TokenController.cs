using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace ToenTest.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenController : Controller
    {
        private readonly JWTTokenOptions _tokenOptions;


        public TokenController(JWTTokenOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
         
        }

        /// <summary>
        /// 生成一个新的 Token
        /// </summary>
        /// <param name="user">用户信息实体</param>
        /// <param name="expire">token 过期时间</param>
        /// <param name="audience">Token 接收者</param>
        /// <returns></returns>
        private string CreateToken( DateTime expire, string audience)
        {
            var Username = "gsw";
            var handler = new JwtSecurityTokenHandler();
            string jti = audience + Username ;
           // jti = jti.GetMd5(); // Jwt 的一个参数，用来标识 Token
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "role"), // 添加角色信息
                new Claim(ClaimTypes.NameIdentifier,"2",ClaimValueTypes.Integer32),
                new Claim("jti",jti,ClaimValueTypes.String) // jti，用来标识 token
            };
            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(Username, "TokenAuth"), claims);
            var token = handler.CreateEncodedJwt(new SecurityTokenDescriptor
            {
                Issuer = "TestIssuer", // 指定 Token 签发者，也就是这个签发服务器的名称
                Audience = audience, // 指定 Token 接收者
                SigningCredentials = _tokenOptions.Credentials,
                Subject = identity,
                Expires = expire
            });
            return token;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user">用户登录信息</param>
        /// <param name="audience">要访问的网站</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string Username,string Password, string audience)
        {
            DateTime expire = DateTime.Now.AddDays(7);

            // 在这里来验证用户的用户名、密码
            //var result = _dbContext.Users.First(u => u.Username == Username && u.Password == Password);
            //if (result == null)
            //{
            //    return Json(new { Error = "用户名或密码错误" });
            //}CreateToken(expire, audience)
            return Json(new { Token = "erwrqewrwefdwefwfewfe" });
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return Json("ok");
        }
        [HttpPost]
        [BearerAuthorize]     
        public IActionResult ABC()
        {
            return Json("aaa");
        }
    }

    /// <summary>
    /// Jwt 验证
    /// </summary>
    public class BearerAuthorizeAttribute : AuthorizeAttribute
    {
        public BearerAuthorizeAttribute() : base("Bearer") { }
    }
}

