using System;
using System.Collections.Generic;
using System.Text;

namespace Do.Domain.AggregatesModel.CaseAggregate
{
    /// <summary>
    /// 实体   案件状态
    /// </summary>
    public class CaseStatus
    {
        public string CaseType
        { get; set; }
        public string Status
        { get; set; }
    }
}
