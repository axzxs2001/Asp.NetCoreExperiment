using Orleans.EventSourcing;
using Orleans.Providers;

using System;
using System.Threading.Tasks;

namespace GrainHub
{
   
    /// <summary>
    /// 数据存储和日志存储
    /// </summary>
    [LogConsistencyProvider(ProviderName = "LogStorage")]
    [StorageProvider(ProviderName = "SettlementStore")]
    public class SettlementGrain : JournaledGrain<SettlementGrainState, ISettlementEvent>, ISettlementGrain
    {
        Random random = new Random();

        public Task<int> GetStatus()
        {
            return Task.FromResult(State.Status);
        }
        public Task<bool> Settlement(SettlementModel settlement)
        {
            //调用事件，为Event Sourse作准备
            switch (this.State.Status)
            {
                case 0:
                    RaiseEvent(
                      new SettlementBeginEvent { SettlementModel = settlement });
                    break;
                case 1:
                    RaiseEvent(new SettlementEndEvent { SettlementModel = settlement });
                    break;
                case 2:
                    RaiseEvent(new SettlementCompleteEvent { SettlementModel = settlement });
                    break;
            }
            ConfirmEvents();
            return Task.FromResult(true);
        }
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        protected override void TransitionState(SettlementGrainState state, ISettlementEvent @event)
        {
            SettlementModel settlement = null;
            try
            {
                switch (@event.ID)
                {
                    case "SettlementBeginEvent":
                        settlement = (@event as SettlementBeginEvent).SettlementModel;
                        if (SettlementBegin(state, settlement))
                        {
                            RaiseEvent(new SettlementEndEvent { SettlementModel = settlement });
                        }
                        break;
                    case "SettlementEndEvent":
                        settlement = (@event as SettlementEndEvent).SettlementModel;
                        if (SettlementEnd(state, settlement))
                        {
                            RaiseEvent(new SettlementCompleteEvent { SettlementModel = settlement });
                        }
                        break;
                    case "SettlementCompleteEvent":
                        settlement = (@event as SettlementCompleteEvent).SettlementModel;
                        if (SettlementComplete(state, settlement))
                        {
                            RaiseConditionalEvent(new SettlementOkEvent());
                        }
                        break;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        bool SettlementBegin(SettlementGrainState state, SettlementModel settlement)
        {
            if (RadomNo())
            {
                throw new Exception("SettlementBegin异常");
            }
            state.Status = 1;
            return true;
        }
        bool SettlementEnd(SettlementGrainState state, SettlementModel settlement)
        {
            if (RadomNo())
            {
                throw new Exception("SettlementEnd异常");
            }
            state.Status = 2;
            return true;
        }
        bool SettlementComplete(SettlementGrainState state, SettlementModel settlement)
        {
            if (RadomNo())
            {
                throw new Exception("SettlementComplete异常");
            }
            state.Status = 3;
            return true;
        }
        bool RadomNo()
        {
            var num = random.Next(1, 4);
            Console.WriteLine($"随机采生的数字是：{num}");
            return num % 3 == 0;
        }
    }
}
