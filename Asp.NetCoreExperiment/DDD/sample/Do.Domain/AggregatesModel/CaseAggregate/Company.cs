using Do.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Do.Domain.AggregatesModel.CaseAggregate
{
    /// <summary>
    /// 值对象  公司信息
    /// </summary>
    public class Company : ValueObject
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
