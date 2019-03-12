
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Text;
using JWTAuthorizePolicy;

namespace AuthenticationService
{
    [AllowAnonymous]
    public class PermissionController : Controller
    {
        /// <summary>
        /// 自定义策略参数
        /// </summary>
        PermissionRequirement _requirement;
        public PermissionController(PermissionRequirement requirement)
        {
            _requirement = requirement;
        }
    
        [HttpPost("/authapi/login")]
        public IActionResult Login(string username, string password)
        {
            var isValidated = (username == "gsw" && password == "111111")|| (username == "ggg" && password == "222222");
            var role=username=="gsw"?"admin" :"system";
            if (!isValidated)
            {
                return new JsonResult(new
                {
                    Status = false,
                    Message = "认证失败"
                });
            }
            else
            {               
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new Claim[] { new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, role), new Claim(ClaimTypes.Expiration ,DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString())};

                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);
             
                var token = JwtToken.BuildJwtToken(claims, _requirement);
                return new JsonResult(token);
            }
        }


   


    }
}
