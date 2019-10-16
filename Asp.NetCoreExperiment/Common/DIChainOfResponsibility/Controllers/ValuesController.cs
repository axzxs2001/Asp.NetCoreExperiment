using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DIChainOfResponsibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// 第一个任务
        /// </summary>
        readonly FirstTask _firstTask;
        public ValuesController(FirstTask  firstTask)
        {
            _firstTask = firstTask;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //调用第一个任务
            _firstTask.ExecuteTask(new TaskParmeter() { TaskID = 1 });
            return new string[] { "value1", "value2" };
        }
    }
}
