using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireTest.Model.Repository
{
    /// <summary>
    /// 权限仓储类
    /// </summary>

    public class Function:IFunction
    {    
        public  void Function1()
        {
            Console.WriteLine("这里要做一些事情");
        }
    }
}
