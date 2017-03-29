using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore_WebPage.Controllers
{
    /// <summary>
    /// Vue测试
    /// </summary>
    public class VueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表格获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("students")]
        public JsonResult Students()
        {
            return new JsonResult(new List<dynamic>() { new { ID = 1, Name = "张三" }, new { ID = 2, Name = "李四" } }, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}