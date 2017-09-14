using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;

namespace Token_WebAPI01.Controllers
{
    [Authorize("Permission")]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        PermissionRequirement requirement;

        public TokenController(IAuthorizationHandler authorizationHander)
        {
            requirement=(authorizationHander as PermissionHandler).Requirement;
        }
 
        [HttpPost]
        public IActionResult Post()
        {
            var username =HttpContext.Request.Form["username"];
            var password = HttpContext.Request.Form["password"];

            var identity1 = GetIdentity(username, password);
            if (identity1 == null)
            {
                return new JsonResult(new
                {
                    Status = false,
                    Message = "认证失败"
                });
            }
            //用户标识
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            //如果是基于角色的授权策略，这里要添加用户
            identity.AddClaim(new Claim(ClaimTypes.Name,username));
            //如果是基于角色的授权策略，这里要添加角色
            identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            var token = GetJwt(username);
            return new JsonResult(token);
        }
    

        private Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var isValidated = username == "gsw" && password == "111111";

            if (isValidated)
            {
                return Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(username, "Token"), new Claim[] { }));

            }
            return Task.FromResult<ClaimsIdentity>(null);
        }
        /// <summary>
        /// get the jwt
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private dynamic GetJwt(string username)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(),
                          ClaimValueTypes.Integer64),
                //用户名
                new Claim(ClaimTypes.Name,username),
                //角色
                new Claim(ClaimTypes.Role,"admin")
            };

            var jwt = new JwtSecurityToken(
                issuer: requirement.Issuer,
                audience: requirement.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(5000)),
                signingCredentials: requirement.SigningCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                Status = true,
                access_token = encodedJwt,
                expires_in = 5000 * 60,
                token_type = "Bearer"
            };
            return response;
        }

    }
}
