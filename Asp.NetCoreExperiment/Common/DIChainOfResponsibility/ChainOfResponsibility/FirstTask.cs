
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace DIChainOfResponsibility
{
    /// <summary>
    /// 第一个任务
    /// </summary>
    public class FirstTask : ITask
    {
        ITask _task;
        readonly ILogger<FirstTask> _logger;
        public FirstTask(ILogger<FirstTask> logger, SecondTask secondTask)
        {
            _logger = logger;
            this.Next(secondTask);
        }
        //错误姿势
        //public FirstTask(ILogger<FirstTask> logger, IEnumerable<ITask> tasks)
        //{
        //    _logger = logger;
        //    foreach (var task in tasks)
        //    {
        //        if (task is SecondTask)
        //        {
        //            this.Next(task);
        //        }
        //    }
        //}

        /// <summary>
        /// 传送下一个方法
        /// </summary>
        /// <param name="parentTask"></param>
        public void Next(ITask task)
        {
            Console.WriteLine($"-------------{task.GetType().Name}.Next()");
            _task = task;
        }
        /// <summary>
        /// 职责链任务方法
        /// </summary>
        /// <param name="taskParmeter">任务内容</param>
        /// <returns></returns>
        public bool ExecuteTask(TaskParmeter taskParmeter)
        {
            var result = SelfTask(taskParmeter);
            return _task.ExecuteTask(taskParmeter) && result;
        }
        bool SelfTask(TaskParmeter taskParmeter)
        {
            _logger.LogInformation("-------------------------------------------FirstTask");
            return true;
        }
    }
}
