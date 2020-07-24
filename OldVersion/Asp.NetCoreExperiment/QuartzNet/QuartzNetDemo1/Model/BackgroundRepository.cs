using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzNetDemo1.Model
{
    public class BackgroundRepository : IBackgroundRepository
    {
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
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":一月结算一次成功执行！");
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
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":一周结算一次成功执行！");
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
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":一月结算两次成功执行！");
            return true;
        }

        /// <summary>
        /// 数据迁移
        /// </summary>
        /// <returns></returns>
        public bool Migration()
        {
            if (new Random().Next(1, 3) % 2 == 0)
            {
                throw new Exception(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":数据迁移异常");
            }
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":数据迁移成功执行！");
            return true;
        }


    }
}
