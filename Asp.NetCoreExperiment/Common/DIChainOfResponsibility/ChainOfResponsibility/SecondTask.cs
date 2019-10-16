
using Microsoft.Extensions.Logging;
using System;

namespace DIChainOfResponsibility
{
    /// <summary>
    /// 第二个任务
    /// </summary>
    public class SecondTask : ParentTask
    {
        readonly ILogger<SecondTask> _logger;
        public SecondTask(ILogger<SecondTask> logger, ThirdTask thirdTask)
        {
            _logger = logger;
            this.Next(thirdTask);
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
            _logger.LogInformation("-------------------------------------------SecondTask");
            return true;

        }


    }
}
