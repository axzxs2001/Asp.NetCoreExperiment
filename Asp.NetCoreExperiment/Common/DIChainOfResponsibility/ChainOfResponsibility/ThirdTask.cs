

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DIChainOfResponsibility
{
    /// <summary>
    /// 第三个任务
    /// </summary>
    public class ThirdTask : ITask
    {
        ITask _task;
        readonly ILogger<ThirdTask> _logger;
        public ThirdTask(ILogger<ThirdTask> logger, EndTask endTask)
        {
            this.Next(endTask);
            _logger = logger;
        }
        //错误姿势
        //public ThirdTask(ILogger<ThirdTask> logger, IEnumerable<ITask> tasks)
        //{
        //    _logger = logger;
        //    foreach (var task in tasks)
        //    {
        //        if (task is EndTask)

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
        /// <param name="transferParmeter">任务内容</param>
        /// <returns></returns>
        public bool ExecuteTask(TaskParmeter taskParmeter)
        {
            var result = SelfTask(taskParmeter);
            return _task.ExecuteTask(taskParmeter) && result;
        }
        bool SelfTask(TaskParmeter taskParmeter)
        {
            _logger.LogInformation("-------------------------------------------ThirdTask");
            return true;
        }
    }
}
