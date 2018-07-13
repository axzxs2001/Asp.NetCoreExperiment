using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace API_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        /// <summary>
        /// 本地Controller化对象
        /// </summary>
        readonly IStringLocalizer<ValuesController> _localizer;
        public ValuesController(IStringLocalizer<ValuesController> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet("/localizer")]
        public IActionResult GetLocalizer()
        {
            Response.Cookies.Append(
                            CookieRequestCultureProvider.DefaultCookieName,
                            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("zh")),
                            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            var dir = new Dictionary<string, string>();
            dir.Add("usernameempty", _localizer["username empty"].Value);
            dir.Add("passwordempty", _localizer["password empty"].Value);
            return new JsonResult(dir);
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //[HttpPost]
        //public IActionResult Login([FromForm]User user)
        //{
        //    if (user.UserName == "gsw" && user.Password == "111111")
        //    {
        //        return new JsonResult(new { result = true, message = "FromForm", data = user.UserName });
        //    }
        //    else
        //    {
        //        return new JsonResult(new { result = false, message = "FromForm用户名或密码不正确" });
        //    }
        //}

        [HttpPost]
        public IActionResult Login([FromBody]User user)
        {
            if (user.UserName == "gsw" && user.Password == "111111")
            {
                return new JsonResult(new { result = true, message = "FromBody", data = user.UserName });
            }
            else
            {
                return new JsonResult(new { result = false, message = "FromBody用户名或密码不正确" });
            }
        }
    }
    public class User
    {
        public string UserName
        { get; set; }
        public string Password
        { get; set; }
    }
}
