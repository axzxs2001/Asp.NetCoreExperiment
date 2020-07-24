using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerDemo.Controllers
{
    /// <summary>
    /// Home控制器
    /// </summary>
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("aaa")]
        public string AAA()
        {
            return "";
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>     
        [HttpGet]
        public IEnumerable<string> BADDD()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>     
        [HttpGet("{id}")]
        public IEnumerable<string> BADDD(int id)
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost("posta{abc}")]
        public IActionResult POSTA([FromBody]ABC abc)
        {
            return Ok();
        }
        [HttpPost("postaA")]
        public IActionResult POSTA([FromBody]string s,int i)
        {
            return Ok();
        }
    }

    public class ABC
    {

    }
}
