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
        public Company(string name, string code, DateTime createTime)
        {
            Name = name;
            Code = code;
            CreateTime = createTime;
        }
        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime CreateTime { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Code;
            yield return CreateTime;
        }
    }
}
