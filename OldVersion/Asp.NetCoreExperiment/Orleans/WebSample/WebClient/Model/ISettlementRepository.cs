using GrainHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Model
{
    public interface ISettlementRepository
    {
        Task<bool> Settlement(SettlementModel settlement);
        Task<int> GetStatus(string settlementID);
    }
}
