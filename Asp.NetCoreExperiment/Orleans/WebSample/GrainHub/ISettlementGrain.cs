using Orleans;
using System;
using System.Threading.Tasks;
namespace GrainHub
{
    public interface ISettlementGrain : IGrainWithGuidKey
    {
        Task<string> Settlement(SettlementModel  settlement);

        Task<int> GetStatus();
    }
}
