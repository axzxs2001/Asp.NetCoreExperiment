
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzNetDemo4.Model
{
    public interface IBackgroundRepository
    {
        /// <summary>
        /// 数据迁移
        /// </summary>
        /// <returns></returns>
        bool Migration();
        /// <summary>
        /// 一月结算一次
        /// </summary>
        /// <returns></returns>
        bool FeeOneTimePerMonth();
        /// <summary>
        /// 一月结算两次
        /// </summary>
        /// <returns></returns>
        bool FeeTwoTimePerMonth();
        /// <summary>
        /// 一周结算一次
        /// </summary>
        /// <returns></returns>
        bool FeeOneTimePerWeek();


   

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool StarPayDailyReport();

 


    }
}
