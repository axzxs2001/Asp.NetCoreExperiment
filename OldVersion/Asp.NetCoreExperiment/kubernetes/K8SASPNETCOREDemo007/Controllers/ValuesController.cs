using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace K8SASPNETCOREDemo007.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                System.IO.File.WriteAllText($"/data/{Environment.MachineName}.txt", $"时间：{DateTime.Now},机器：{Environment.MachineName},版本号：{ Environment.OSVersion.VersionString}", System.Text.Encoding.UTF8);
                return new string[] { "K8SASPNETCOREDemo007--测试服务" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"), "所在服务器：" + Environment.MachineName + " OS:" + Environment.OSVersion.VersionString };
            }
            catch (Exception exc)
            {
                return new string[] { "异常：" + exc.Message };
            }
        }


        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var avalue = System.IO.File.ReadAllText($"/data/{Environment.MachineName}.txt", System.Text.Encoding.UTF8);
            return avalue;
        }
    }
}
