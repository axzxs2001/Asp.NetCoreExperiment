using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestHotDI
{
    public interface IMyService
    {
        string Print();
    }

    public class MyService : IMyService
    {
        public string Print()
        {
            return DateTime.Now.ToString();
        }
    }

}
