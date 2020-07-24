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
        readonly ITask _task;

        public ValuesController(FirstTask firstTask)
        {
            _task = firstTask;
        }
        //错误姿势
        //public ValuesController(IEnumerable<ITask> tasks)
        //{
        //    foreach (var task in tasks)
        //    {
        //        if (task is EndTask)
        //        {
        //            _task = task;
        //        }
        //    }
        //}

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //调用第一个任务
            _task.ExecuteTask(new TaskParmeter() { TaskID = 1 });
     
            return new string[] { "value1", "value2" };
        }
    }
}
