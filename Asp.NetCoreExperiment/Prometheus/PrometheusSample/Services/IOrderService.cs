using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrometheusSample.Services
{
    public interface IOrderService
    {
        Task<bool> Register(string username);
    }
}
