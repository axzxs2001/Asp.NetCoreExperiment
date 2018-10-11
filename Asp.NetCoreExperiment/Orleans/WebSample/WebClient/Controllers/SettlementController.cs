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
            var status = 0;
            var times = 1;
            do
            {
                var result = await _settlementRepository.Settlement(new GrainHub.SettlementModel
                {
                    SettlementID = settlementID,
                    SettlementCycle = "2018-08-01 to 2018-08-31",
                    SettlementTime = DateTime.UtcNow
                });
                System.Threading.Thread.Sleep(500);
                status = await _settlementRepository.GetStatus(settlementID);
                if (status == 3)
                {
                    break;
                }
                else
                if (times == 3)
                {
                    break;
                }
                else
                {
                    times++;
                }
            } while (true);
            return new JsonResult(new { Status = status });
        }



    }
}
