using System;
using System.Collections.Generic;
using System.Text;

namespace GrainHub
{
    /// <summary>
    /// 数据模型
    /// </summary>
    [Serializable]
    public  class SettlementModel
    {
        /// <summary>
        /// 结算ID
        /// </summary>
        public string SettlementID
        { get; set; }
        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime SettlementTime
        { get; set; }
        /// <summary>
        /// 结算周期
        /// </summary>
        public string SettlementCycle
        { get; set; }
    
    }
}
