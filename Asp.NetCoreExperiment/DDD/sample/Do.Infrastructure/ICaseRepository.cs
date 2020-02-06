using System;

namespace Do.Infrastructure
{
    public interface ICaseRepository
    {
        bool AddCase(IEntity caseEntity);
        bool ModifyCase(IEntity caseEntity);
        bool RemoveCase(int id);
    }
}
