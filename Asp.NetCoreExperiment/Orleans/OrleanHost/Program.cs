using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleanHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = Orleans.Runtime.Configuration.ClusterConfiguration.LocalhostPrimarySilo(2234, 1234);
            var silohost = new Orleans.Runtime.Host.SiloHost("Ba", config);
            silohost.InitializeOrleansSilo();
            silohost.StartOrleansSilo();

            if(silohost.IsStarted)
            {
                Console.WriteLine("silohost 启动成功");
            }
            else
            {
                Console.WriteLine("启动失败");
            }
            Console.ReadKey();
        }
    }
}
