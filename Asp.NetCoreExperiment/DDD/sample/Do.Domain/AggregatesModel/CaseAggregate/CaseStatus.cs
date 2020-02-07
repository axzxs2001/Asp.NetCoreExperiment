using Do.Domain.Respository;
using Do.Domain.Respository.Model;
using Do.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Do.Domain.AggregatesModel.CaseAggregate
{
    /// <summary>
    /// 实体   案件状态
    /// </summary>
    public class CaseStatus : DomainEntity
    {
        readonly CaseRepository _caseRepository;
        public CaseStatus()
        {
            _caseRepository = new CaseRepository();
        }
        public string CaseType
        { get; set; }
        public string Status
        { get; set; }

        public bool AddCaseStatus()
        {
            Console.WriteLine("案件状态实体内AddCaseStatus");
            return _caseRepository.AddCaseStatus(new CaseStatusModel { CaseType = CaseType, Status = Status });
        }
    }
}
