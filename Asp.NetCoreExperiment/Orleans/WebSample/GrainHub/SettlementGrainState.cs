using System;

namespace GrainHub
{
    [Serializable]
    public class SettlementGrainState
    {
        /// <summary>
        /// 结算ID
        /// </summary>
        public string SettlementID
        { get; set; }
        /// <summary>
        /// 创建时间，默认值为当前时间
        /// </summary>
        public DateTime CreateTime
        {
            get; set;
        } = DateTime.UtcNow;

        /// <summary>
        /// 状态，默认值为0
        /// </summary>
        public int Status
        {
            get; set;
        } = 0;
    }
}
