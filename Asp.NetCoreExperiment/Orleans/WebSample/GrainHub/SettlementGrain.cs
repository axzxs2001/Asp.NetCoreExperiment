using Orleans;
using Orleans.Providers;
using System;
using System.Threading.Tasks;

namespace GrainHub
{
    [StorageProvider(ProviderName = "SettlementStore")]
    public class SettlementGrain :Grain, ISettlementGrain
    {
        public Task<bool> Settlement(DateTime dateTime)
        {
            Console.WriteLine(dateTime);
            return Task.FromResult(true);
        }
    }
}
