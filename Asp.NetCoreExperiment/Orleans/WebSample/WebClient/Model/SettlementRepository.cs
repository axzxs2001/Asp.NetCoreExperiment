using GrainHub;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Model
{
    public class SettlementRepository: ISettlementRepository
    {

        readonly ILogger<SettlementRepository> _logger;
        readonly IClientCreater _clientCreater;
        public SettlementRepository(ILogger<SettlementRepository> logger, IClientCreater clientCreater)
        {
            _logger = logger;
            _clientCreater = clientCreater;
        }


        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Settlement()
        {
            var client = await _clientCreater.CreateClient("SettlementClusterID", "settlementServiceID");
            var settlement = client.GetGrain<ISettlementGrain>(new Guid());
            return await settlement.Settlement(DateTime.Now);
        }
    }
}
