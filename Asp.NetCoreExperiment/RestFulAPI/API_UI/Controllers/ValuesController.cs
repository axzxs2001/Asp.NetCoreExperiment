using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
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
