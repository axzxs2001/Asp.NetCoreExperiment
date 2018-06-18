using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzNetDemo1.Model
{
    public class MigrationRepository : IMigrationRepository
    {
        public bool Migration()
        {
            if (new Random().Next(1,3) % 2 == 0)
            {
                throw new Exception(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":异常");
            }
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":成功执行！");
            return true;
        }
    }
}
