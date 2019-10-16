using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace DIChainOfResponsibility
{
    /// <summary>
    /// 最后的任务
    /// </summary>
    public class EndTask : ParentTask
    {
        readonly ILogger<EndTask> _logger;
        public EndTask(ILogger<EndTask> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 职责链任务方法
        /// </summary>
        /// <param name="taskParmeter">任务内容</param>
        /// <returns></returns>
        public override bool ExecuteTask(TaskParmeter taskParmeter)
        {
            _logger.LogInformation("-------------------------------------------EndTask");
            return true;
        }
    }
}
