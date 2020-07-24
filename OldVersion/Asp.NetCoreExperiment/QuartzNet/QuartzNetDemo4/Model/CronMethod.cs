using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzNetDemo4.Model.DataModel
{
    /// <summary>
    /// 表达式配置文件
    /// </summary>
    public class CronMethod
    {
        public string MethodName { get; set; }
        public string CronExpression { get; set; }
    }
}
