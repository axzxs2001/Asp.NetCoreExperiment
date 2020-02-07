using Do.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Do.Domain.Respository;
using Do.Domain.Respository.Model;

namespace Do.Domain.AggregatesModel.CaseAggregate
{
    /// <summary>
    /// 聚合根  案件
    /// </summary>
    public class Case : DomainEntity, IAggregateRoot
    {
        readonly ICaseRepository _caseRepository;
        public Case(string userID, Company company)
        {
            _caseRepository = new CaseRepository();
            UserID = userID;
            Company = company;
            CreateTime = DateTimeOffset.UtcNow;
            _pays = new List<Pay>();
        }
        private readonly List<Pay> _pays;
        public string UserID { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public Company Company { get; set; }
        public CaseStatus Status { get; private set; }

        public bool AddCase()
        {
            Console.WriteLine(" 聚合根  案件内AddCase");
            //添加案件
            return _caseRepository.AddCase(new CaseModel { Company = System.Text.Json.JsonSerializer.Serialize(Company), CreateTime = CreateTime, ID = ID, UserID = UserID });
        }

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
