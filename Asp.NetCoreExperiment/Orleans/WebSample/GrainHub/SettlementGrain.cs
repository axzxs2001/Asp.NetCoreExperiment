using System;
using System.Threading.Tasks;

namespace GrainHub
{
    public class SettlementGrain : ISettlementGrain
    {
        public Task<bool> Settlement(DateTime dateTime)
        {
            Console.WriteLine(dateTime);
            return Task.FromResult(true) ;
        }
    }
}
