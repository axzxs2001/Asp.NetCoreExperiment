using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzNetDemo4.Model
{
    /// <summary>
    /// 自定义项目异常
    /// </summary>
    public class QuartzNetDemo4Exception : ApplicationException
    {
        public QuartzNetDemo4Exception(string message) : base(message)
        {

        }
    }
}
