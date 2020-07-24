using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{ 
   public class BasicGrain :Grain, IGrains.IBasic
    {
        public Task<string> SayHello(string hellostr)
        {
            Console.WriteLine($"{DateTime.Now},{hellostr}");
            return Task.FromResult<string>("完了");
        }
    }
}
