using System;

namespace GrainHub
{
    public interface ISettlementEvent
    {
        string ID { get; }
    }
    [Serializable]
    public class SettlementBeginEvent : ISettlementEvent
    {
        public string ID { get { return "SettlementBeginEvent"; } }   

        public SettlementModel SettlementModel { get; set; }
    }
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
}
