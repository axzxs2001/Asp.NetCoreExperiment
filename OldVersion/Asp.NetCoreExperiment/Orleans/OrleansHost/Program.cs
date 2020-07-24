using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ClusterConfiguration.LocalhostPrimarySilo(2234,22222);
            var silohost = new SiloHost("Ba", config);
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
