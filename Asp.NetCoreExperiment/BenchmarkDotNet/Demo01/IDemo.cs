using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01
{
    public interface IDemo
    {
        void Run();
    }
    public interface IDemoAsync
    {
        Task RunAsync();
    }
}
