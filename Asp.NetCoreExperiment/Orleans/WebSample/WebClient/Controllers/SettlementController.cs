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

        [HttpPost("/settlement")]
        public async Task<IActionResult> Settlement([FromBody]string settlementID)
        {
            var result = await _settlementRepository.Settlement(new GrainHub.SettlementModel
            {
                SettlementID = settlementID,
                SettlementCycle = "2018-08-01 to 2018-08-31",
                SettlementTime = DateTime.UtcNow             
            });
            return new JsonResult(new { Result = result });
        }

        [HttpGet("/status")]
        public async Task<IActionResult> Status(string settlementID)
        {
            var result = await _settlementRepository.GetStatus(settlementID);
            return new JsonResult(new { Result = result });
        }


    }
}
