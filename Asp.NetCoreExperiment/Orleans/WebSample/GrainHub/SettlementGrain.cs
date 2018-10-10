using Orleans;
using Orleans.Providers;
using System;
using System.Threading.Tasks;

namespace GrainHub
{
    [StorageProvider(ProviderName = "SettlementStore")]
    public class SettlementGrain : Grain<SettlementGrainState>, ISettlementGrain
    {
        public async Task<string> Settlement(SettlementModel settlement)
        {
            await base.ReadStateAsync();
            if (State.SettlementID == settlement.SettlementID)
            {
                
                switch (State.Status)
                {
                    case 0:
                        await SettlementBegin(settlement);
                        break;
                    case 1:
                        await SettlementEnd(settlement);
                        break;
                    case 2:
                        await SettlementComplete(settlement);
                        break;
                }
                return await Task.FromResult(Newtonsoft.Json.JsonConvert.SerializeObject(State));
            }
            else
            {
                if (await SettlementBegin(settlement))
                {
                    return await Task.FromResult(Newtonsoft.Json.JsonConvert.SerializeObject(State));
                }
                else
                {
                    throw new Exception("出错了!");
                }
            }
        }

        async Task<bool> SettlementBegin(SettlementModel settlement)
        {
            if (RadomNo())
            {
                throw new Exception("SettlementBegin异常");
            }

            State.CreateTime = DateTime.UtcNow;
            State.Status = 1;
            State.SettlementID = settlement.SettlementID;
            await base.WriteStateAsync();
            return await SettlementEnd(settlement);
        }
        async Task<bool> SettlementEnd(SettlementModel settlement)
        {
            if (RadomNo())
            {
                throw new Exception("SettlementEnd异常");
            }
            State.CreateTime = DateTime.UtcNow;
            State.Status = 2;
            State.SettlementID = settlement.SettlementID;
            await base.WriteStateAsync();
            return await SettlementComplete(settlement);
        }
        async Task<bool> SettlementComplete(SettlementModel settlement)
        {
            if (RadomNo())
            {
                throw new Exception("SettlementComplete异常");
            }
            State.CreateTime = DateTime.UtcNow;
            State.Status = 3;
            State.SettlementID = settlement.SettlementID;
            await base.WriteStateAsync();
            return true;
        }

        bool RadomNo()
        {
            var random = new Random();
            return random.Next(1, 4) % 3 == 0 ? true : false;
        }
    }
}
