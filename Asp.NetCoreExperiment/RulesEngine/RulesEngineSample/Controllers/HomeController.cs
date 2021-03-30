using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RulesEngineSample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RulesEngineSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ICouponService _couponService;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICouponService couponService)
        {
            _couponService = couponService;
            _logger = logger;
        }

        [HttpGet("/selectcoupon")]
        public async Task<string> SelectCoupon()
        {
            var result = await _couponService.SelectCouponAsync();
            return result;
        }


        [HttpGet("/getamount")]
        public string GetAmount(string code)
        {
            var result = _couponService.GetOrderAmount(code);
            return result;
        }

    }
}
