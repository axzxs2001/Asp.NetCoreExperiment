using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grains;
using IGrains;
using Orleans.Runtime.Configuration;

namespace Client
{
    class Program
    {
         static void Main(string[] args)
        {
            Run();
            Console.ReadKey();
        }
        static async void Run()
        {
            var config = ClientConfiguration.LocalhostSilo(22222);
            GrainClient.Initialize(config);
       
            var agrain = GrainClient.GrainFactory.GetGrain<IGrains.IBasic>(321) ;
            var result = await agrain.SayHello("你好");
            Console.WriteLine(result);
        }
    }
}
