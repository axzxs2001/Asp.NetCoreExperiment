using Orleans.EventSourcing;
using Orleans.Providers;

using System;
using System.Threading.Tasks;

namespace GrainHub
{
    [LogConsistencyProvider(ProviderName = "LogStorage")]
    [StorageProvider(ProviderName = "SettlementStore")]
    public class SettlementGrain : JournaledGrain<SettlementGrainState, ISettlementEvent>, ISettlementGrain
    {
        public async Task<string> Settlement(SettlementModel settlement)
        {
            if (this.State.SettlementID == settlement.SettlementID)
            {
                switch (this.State.Status)
                {
                    case 0:
                        RaiseEvent(new SettlementBeginEvent
                        {
                            SettlementModel = settlement
                        });
                        RaiseEvent(new SettlementEndEvent
                        {
                            SettlementModel = settlement
                        });
                        RaiseEvent(new SettlementCompleteEvent
                        {
                            SettlementModel = settlement
                        });
                        break;
                    case 1:
                        RaiseEvent(new SettlementEndEvent
                        {
                            SettlementModel = settlement
                        });
                        RaiseEvent(new SettlementCompleteEvent
                        {
                            SettlementModel = settlement
                        });
                        break;
                    case 2:
                        RaiseEvent(new SettlementCompleteEvent
                        {
                            SettlementModel = settlement
                        });
                        break;
                }
            }
            else
            {
                this.State.Status = 0;
                switch (this.State.Status)
                {
                    case 0:
                        RaiseEvent(new SettlementBeginEvent
                        {
                            SettlementModel = settlement
                        });
                        RaiseEvent(new SettlementEndEvent
                        {
                            SettlementModel = settlement
                        });
                        RaiseEvent(new SettlementCompleteEvent
                        {
                            SettlementModel = settlement
                        });
                        break;
                    case 1:
                        RaiseEvent(new SettlementEndEvent
                        {
                            SettlementModel = settlement
                        });
                        RaiseEvent(new SettlementCompleteEvent
                        {
                            SettlementModel = settlement
                        });
                        break;
                    case 2:
                        RaiseEvent(new SettlementCompleteEvent
                        {
                            SettlementModel = settlement
                        });
                        break;
                }
            }
            await ConfirmEvents();
            return null;
        }
        protected override void TransitionState(SettlementGrainState state, ISettlementEvent @event)
        {

            switch (@event.ID)
            {
                case "SettlementBeginEvent":
                    SettlementBegin((@event as SettlementBeginEvent).SettlementModel).Wait();
                    break;
                case "SettlementEndEvent":
                    SettlementEnd((@event as SettlementEndEvent).SettlementModel).Wait();
                    break;
                case "SettlementCompleteEvent":
                    SettlementComplete((@event as SettlementCompleteEvent).SettlementModel).Wait();
                    break;
            }
        }

        async Task<bool> SettlementBegin(SettlementModel settlement)
        {
            //if (RadomNo())
            //{
            //    throw new Exception("SettlementBegin异常");
            //}
            State.Status = 1;
            State.SettlementID = settlement.SettlementID;
            return await Task.FromResult(true);
        }
        async Task<bool> SettlementEnd(SettlementModel settlement)
        {
            //if (RadomNo())
            //{
            //    throw new Exception("SettlementEnd异常");
            //}
            State.Status = 2;
            State.SettlementID = settlement.SettlementID;
            return await Task.FromResult(true);
        }
        async Task<bool> SettlementComplete(SettlementModel settlement)
        {
            //if (RadomNo())
            //{
            //    throw new Exception("SettlementComplete异常");
            //}
            State.Status = 3;
            State.SettlementID = settlement.SettlementID;
            return await Task.FromResult(true);
        }

        bool RadomNo()
        {
            var random = new Random();
            return random.Next(1, 4) % 3 == 0 ? true : false;
        }
    }
}
