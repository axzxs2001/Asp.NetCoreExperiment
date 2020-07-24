
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Quartz.Impl;
using Microsoft.Extensions.Logging;

namespace QuartzNetDemo4.Model
{
    /// <summary>
    /// 后台任务
    /// </summary>
    public class BackgroundJob : IJob
    {
        readonly ILogger<BackgroundJob> _logger;
        IBackgroundRepository _backgroundRepository;
        public BackgroundJob(IBackgroundRepository backgroundRepository, ILogger<BackgroundJob> logger)
        {
            _logger = logger;
            _backgroundRepository = backgroundRepository;
        }
        public  Task Execute(IJobExecutionContext context)
        {
            try
            {
                if (context.JobDetail is JobDetailImpl)
                {
                    var names = (context.JobDetail as Quartz.Impl.JobDetailImpl)?.Name.Split('_');
                    var method = names.Length > 1 ? names[1] : "";
                    if (!string.IsNullOrEmpty(method))
                    {
                        var methodInfo = typeof(IBackgroundRepository).GetMethod(method);
                        var result =methodInfo.Invoke(_backgroundRepository, new object[0]);                       
                        var runResult = false;
                        //成功执行
                        if (bool.TryParse(result.ToString(), out runResult) && runResult)
                        {
                            _logger.LogInformation($"BackgroundJob.Execute成功，方法：{method}");
                        }
                        else//不成功执行
                        {
                            _logger.LogInformation($"BackgroundJob.Execute失败，方法：{method}");
                        }
                    }
                }
            }
            catch (Exception exc)
            {           
                _logger.LogCritical($"{DateTime.Now}:{exc.Message}");
                if(exc.InnerException!=null)
                {
                    _logger.LogCritical($"     {DateTime.Now}:{exc.InnerException.Message}");
                }             
            }
            return Task.CompletedTask;
        }
    }
}
