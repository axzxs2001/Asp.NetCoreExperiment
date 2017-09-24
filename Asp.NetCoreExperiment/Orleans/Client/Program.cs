using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            Run();
            Console.ReadKey();
        }
        static async void Run()
        {
            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo(1234);
            GrainClient.Initialize(config);
            var agrain = GrainClient.GrainFactory.GetGrain<IGrains.IBasic>(314);
            var result = await agrain.SayHello("你好");
            Console.WriteLine(result);
        }
    }
}
