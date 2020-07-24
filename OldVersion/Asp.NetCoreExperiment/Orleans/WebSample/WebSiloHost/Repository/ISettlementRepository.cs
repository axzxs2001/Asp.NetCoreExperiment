using System;
using System.Threading.Tasks;

namespace WebSiloHost.Repository
{
    public interface ISettlementRepository
    {
        Task<bool> Settlement(DateTime dateTime);
    }
}
