
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DIChainOfResponsibility
{
    /// <summary>
    /// 第二个任务
    /// </summary>
    public class SecondTask : ITask
    {
        ITask _task;
        readonly ILogger<SecondTask> _logger;
        public SecondTask(ILogger<SecondTask> logger, ThirdTask task)
        {
            _logger = logger;
            this.Next(task);
        }
        //错误姿势
        //public SecondTask(ILogger<SecondTask> logger, IEnumerable<ITask> tasks)
        //{
        //    _logger = logger;
        //    foreach (var task in tasks)
        //    {
        //        if (task is ThirdTask)

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
            _logger.LogInformation("-------------------------------------------SecondTask");
            return true;

        }


    }
}
