using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebClient.Model;

namespace WebClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettlementController : ControllerBase
    {

        readonly ISettlementRepository _settlementRepository;
        public SettlementController(ISettlementRepository settlementRepository)
        {
            _settlementRepository = settlementRepository;
        }


        [HttpGet("/settlement")]
        public async Task<IActionResult> Settlement()
        {
            var result = await _settlementRepository.Settlement();
            return new JsonResult(new { Result = result });
        }
    }
}
