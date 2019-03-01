using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace K8SASPNETCOREDemo004.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                System.IO.File.WriteAllText($"/data/{Environment.MachineName}.txt", $"时间：{DateTime.Now},机器：{Environment.MachineName},版本号：{ Environment.OSVersion.VersionString}", System.Text.Encoding.UTF8);       
                return new string[] { "K8SASPNETCOREDemo004--测试服务" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"), "所在服务器：" + Environment.MachineName + " OS:" + Environment.OSVersion.VersionString };
            }
            catch (Exception exc)
            {
                return new string[] { "异常：" + exc.Message };
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {           
            var avalue = System.IO.File.ReadAllText($"/data/{Environment.MachineName}.txt", System.Text.Encoding.UTF8);          
            return avalue;
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
    }
}
