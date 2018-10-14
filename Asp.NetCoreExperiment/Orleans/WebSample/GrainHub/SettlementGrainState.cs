using System;

namespace GrainHub
{
    /// <summary>
    /// Grain状态
    /// </summary>
    [Serializable]
    public class SettlementGrainState
    {     
        /// <summary>
        /// 状态，默认值为0
        /// </summary>
        public int Status
        {
            get; set;
        } = 0;
    }
}
