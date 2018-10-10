using GrainHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Model
{
    public interface ISettlementRepository
    {
        Task<string> Settlement(SettlementModel settlement);
     
    }
}
