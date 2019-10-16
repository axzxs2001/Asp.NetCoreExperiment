
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
namespace DIChainOfResponsibility
{
    /// <summary>
    /// 第一个任务
    /// </summary>
    public class FirstTask : ParentTask
    {
        readonly ILogger<FirstTask> _logger;
        public FirstTask(ILogger<FirstTask> logger, SecondTask  secondTask)
        {
            _logger = logger;
            this.Next(secondTask);
        }

        /// <summary>
        /// 职责链任务方法
        /// </summary>
        /// <param name="taskParmeter">任务内容</param>
        /// <returns></returns>
        public override bool ExecuteTask(TaskParmeter taskParmeter)
        {
            var result = SelfTask(taskParmeter);
            return _parentTask.ExecuteTask(taskParmeter) && result;
        }
        bool SelfTask(TaskParmeter taskParmeter)
        {
            _logger.LogInformation("-------------------------------------------FirstTask");
            return true;
        }
    }
}
