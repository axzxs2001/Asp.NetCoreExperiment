using System;

namespace GrainHub
{
    /// <summary>
    /// 事件接口
    /// </summary>
    public interface ISettlementEvent
    {
        string ID { get; }
    }
    /// <summary>
    /// 开始结算事件
    /// </summary>
    [Serializable]
    public class SettlementBeginEvent : ISettlementEvent
    {
        public string ID { get { return "SettlementBeginEvent"; } }   

        public SettlementModel SettlementModel { get; set; }
    }
    /// <summary>
    /// 结束结算事件
    /// </summary>
    [Serializable]
    public class SettlementEndEvent : ISettlementEvent
    {
        public string ID { get { return "SettlementEndEvent"; } }

        public SettlementModel SettlementModel { get; set; }
    }
    [Serializable]
    public class SettlementCompleteEvent : ISettlementEvent
    {
        public string ID { get { return "SettlementCompleteEvent"; } }
        public SettlementModel SettlementModel { get; set; }
    }
    /// <summary>
    /// 结算完成事件
    /// </summary>
    [Serializable]
    public class SettlementOkEvent : ISettlementEvent
    {
        public string ID { get { return "SettlementOkEvent"; } }
        public SettlementModel SettlementModel { get; set; }
    }
}
