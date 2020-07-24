using GrainHub;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebClient.Model
{
    public class SettlementRepository : ISettlementRepository
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
        public async Task<bool> Settlement(SettlementModel settlement)
        {
            var client = await _clientCreater.CreateClient();
            var guid = Guid.Parse(settlement.SettlementID);
            var settlementGrain = client.GetGrain<ISettlementGrain>(guid);
            return await settlementGrain.Settlement(settlement);

        }

        public async Task<int> GetStatus(string settlementID)
        {
            var client = await _clientCreater.CreateClient();
            var guid = Guid.Parse(settlementID);
            var settlementGrain = client.GetGrain<ISettlementGrain>(guid);
            return await settlementGrain.GetStatus();

        }

    }
}
