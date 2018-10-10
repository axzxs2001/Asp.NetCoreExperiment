using System;
using System.Collections.Generic;
using System.Text;

namespace GrainHub
{
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
