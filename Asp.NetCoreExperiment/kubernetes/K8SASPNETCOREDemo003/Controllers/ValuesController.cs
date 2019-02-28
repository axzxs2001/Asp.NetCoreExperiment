using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace K8SASPNETCOREDemo003.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var value = "";
            try
            {
                var userName = System.IO.File.ReadAllText("/projected-volume/username.txt", System.Text.Encoding.UTF8);
                var password = System.IO.File.ReadAllText("/projected-volume/password.txt", System.Text.Encoding.UTF8);
                var person = System.IO.File.ReadAllText("/persondata/data.txt", System.Text.Encoding.UTF8);
                value = $"用户名：{userName }         密码：{password}          个人信息：{person}";

            }
            catch (Exception exc)
            {
                value += $"{exc.Message}";
            }
            return new string[] { "K8SASPNETCOREDemo003测试服务" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"), "所在服务器：" + Environment.MachineName + " OS:" + Environment.OSVersion.VersionString, value };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("/health")]
        public IActionResult Helath()
        {
            if (DateTime.Now.Minute % 30 == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
    }
}
