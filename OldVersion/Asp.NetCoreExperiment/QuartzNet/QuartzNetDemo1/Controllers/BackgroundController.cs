using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuartzNetDemo1.Model;

namespace QuartzNetDemo1.Controllers
{
    [Route("api/[controller]")]
    public class BackgroundController : Controller
    {
        IBackgroundRepository _backgroundRepository;

        public BackgroundController(IBackgroundRepository backgroundRepository)
        {
            _backgroundRepository = backgroundRepository;
        }
     
        [HttpGet("migration")]
        public IActionResult Migration()
        {
            var result = _backgroundRepository.Migration();
            return Ok(result);
        }     
        [HttpGet("onepermonth")]
        public IActionResult OnePerMonth()
        {
            var result = _backgroundRepository.FeeOneTimePerMonth();
            return Ok(result);
        }
        [HttpGet("twopermonth")]
        public IActionResult TwoPerMonth()
        {
            var result = _backgroundRepository.FeeTwoTimePerMonth();
            return Ok(result);
        }
        [HttpGet("twoperweek")]
        public IActionResult TwoPerWeek()
        {
            var result = _backgroundRepository.FeeOneTimePerWeek();
            return Ok(result);
        }
    }
}
