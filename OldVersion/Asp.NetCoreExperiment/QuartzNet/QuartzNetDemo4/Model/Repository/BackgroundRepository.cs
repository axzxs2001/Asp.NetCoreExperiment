
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace QuartzNetDemo4.Model
{
    public class BackgroundRepository : IBackgroundRepository
    {
        readonly ILogger<BackgroundRepository> _logger;
        readonly IServiceProvider _serviceProvider;
        public BackgroundRepository(ILogger<BackgroundRepository> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        #region 其他方法
        /// <summary>
        /// 一月结算一次
        /// </summary>
        /// <returns></returns>
        public bool FeeOneTimePerMonth()
        {
            if (new Random().Next(1, 3) % 2 == 0)
            {
                throw new Exception(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":一月结算一次异常");
            }
            _logger.LogInformation(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":一月结算一次成功执行！");
            return true;
        }
        /// <summary>
        /// 一周结算一次
        /// </summary>
        /// <returns></returns>
        public bool FeeOneTimePerWeek()
        {
            if (new Random().Next(1, 3) % 2 == 0)
            {
                throw new Exception(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":一周结算一次异常");
            }
            _logger.LogInformation(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":一周结算一次成功执行！");
            return true;
        }
        /// <summary>
        /// 一月结算两次
        /// </summary>
        /// <returns></returns>
        public bool FeeTwoTimePerMonth()
        {
            if (new Random().Next(1, 3) % 2 == 0)
            {
                throw new Exception(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":一月结算两次异常");
            }
            _logger.LogInformation(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":一月结算两次成功执行！");
            return true;
        }

        /// <summary>
        /// 数据迁移
        /// </summary>
        /// <returns></returns>
        public bool Migration()
        {
            return true;
        }
    

      
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool StarPayDailyReport()
        {

            Console.WriteLine($"{DateTime.Now}_______StarPayDailyReport");
            return true;


        }
       
        #endregion
    }
}
