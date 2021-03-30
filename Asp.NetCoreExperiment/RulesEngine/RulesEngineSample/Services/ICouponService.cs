using Microsoft.Extensions.Logging;
using RulesEngine.Extensions;
using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RulesEngineSample.Services
{
    public interface ICouponService
    {

        Task<string> SelectCouponAsync();
        string GetOrderAmount(string code);
    }

}
