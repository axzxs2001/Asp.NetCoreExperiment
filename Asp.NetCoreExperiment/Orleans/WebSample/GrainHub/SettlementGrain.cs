using System;
using System.Threading.Tasks;

namespace GrainHub
{
    public class SettlementGrain : ISettlementGrain
    {
        public Task<bool> Settlement(DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}
