using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
            var result = await _settlementRepository.Settlement(new GrainHub.SettlementModel
            {
                SettlementID = "gsw_settlement",
                SettlementCycle = "2018-08-01 to 2018-08-31",
                SettlementTime = DateTime.UtcNow
            });
            return new JsonResult(new { Result = result });
        }


    }
}
