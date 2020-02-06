using Do.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Do.Domain.AggregatesModel.CaseAggregate
{
    /// <summary>
    /// 聚合根  案件
    /// </summary>
    public class Case : DomainEntity, IAggregateRoot
    {
        public Case(string userID, Company company)
        {
            UserID = userID;
            Company = company;
            CreateTime = DateTimeOffset.UtcNow;
            _pays = new List<Pay>();
        }
        private readonly List<Pay> _pays;
        public string UserID { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public Company Company { get; set; }
        public CaseStatus Status { get;private set; }

        public void AddPay(Pay pay)
        {
            _pays.Add(pay);
        }
        public void RemovePay(Pay pay)
        {
            _pays.Remove(pay);
        }
        public Pay GetPay(string payName)
        {
            return _pays.SingleOrDefault(s => s.PayName == payName);
        }
    }
}
