using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGrains
{
     public interface IBasic:IGrainWithIntegerKey
    {
        Task<string> SayHello(string hellostr);
    }
}
