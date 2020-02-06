using System;
using System.Collections.Generic;
using System.Text;

namespace Do.Infrastructure
{
    public class CaseRepository : ICaseRepository
    {
        public bool AddCase(IEntity caseEntity)
        {
            Console.WriteLine("add caseEntity");
            return true;
        }

        public bool ModifyCase(IEntity caseEntity)
        {
            Console.WriteLine("modify caseEntity");
            return true;
        }

        public bool RemoveCase(int id)
        {
            Console.WriteLine("remove caseEntity");
            return true;
        }
    }
}
