using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerDemo.Controllers
{
    /// <summary>
    /// Values控制器
    /// </summary>
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns> 
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var url = Url.Action("list");
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// 按ID获取
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int? id)
        {
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="s">字符</param>
        /// <param name="i">整理</param>
        /// <returns></returns>
        [HttpPost("list")]
        public List<string> GetList()
        {
            return null;
        }
    }
}
