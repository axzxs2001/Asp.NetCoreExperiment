using Do.Domain.Respository.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Do.Domain.Respository
{
    /// <summary>
    /// 案件仓储
    /// </summary>
    public class CaseRepository : ICaseRepository
    {
        #region 案件仓储
        public bool AddCase(CaseModel @case)
        {
            Console.WriteLine("add CaseModel");
            return true;
        }

        public bool ModifyCase(CaseModel @case)
        {
            Console.WriteLine("modify CaseModel");
            return true;
        }

        public bool RemoveCase(int id)
        {
            Console.WriteLine("remove CaseModel");
            return true;
        }
        #endregion

        #region 案件状态
        public bool AddCaseStatus(CaseStatusModel  caseStatus)
        {
            Console.WriteLine("add CaseStatusModel");
            return true;
        }

        public bool ModifyCaseStatus(CaseStatusModel caseStatus)
        {
            Console.WriteLine("modify CaseStatusModel");
            return true;
        }

        public bool RemoveCaseStatus(int id)
        {
            Console.WriteLine("remove CaseStatusModel");
            return true;
        }
        #endregion
    }
}
