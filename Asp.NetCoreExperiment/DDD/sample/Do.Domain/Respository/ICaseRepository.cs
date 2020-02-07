using Do.Domain.Respository.Model;
using System;

namespace Do.Domain.Respository
{
    /// <summary>
    /// 案件仓储接口
    /// </summary>
    public interface ICaseRepository
    {
        bool AddCase(CaseModel  @case);
        bool ModifyCase(CaseModel  @case);
        bool RemoveCase(int id);
    }
}
