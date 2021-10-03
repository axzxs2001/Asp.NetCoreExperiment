using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace NoticeSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        { 
            _logger = logger;
        }

        [HttpPost("/ordercomplete")]
        public async Task<IActionResult> OrderComplete()
        {
            try
            {
                _logger.LogInformation("NoticeSystem OrderComplete runing¡­¡­");
                using var reader = new StreamReader(Request.Body, System.Text.Encoding.UTF8);
                var content = await reader.ReadToEndAsync();
                var pubBody = Newtonsoft.Json.JsonConvert.DeserializeObject<PubBody>(content);
                _logger.LogInformation($"---------  HostName:{Dns.GetHostName()},OrderNo:{pubBody?.data.OrderNo},OrderAmount:{pubBody?.data.Amount},OrderTime:{pubBody?.data.OrderTime} -----------");
                await Task.Delay(200);
                _logger.LogInformation($"subscription notice complete");
                _logger.LogInformation($"return  SUCCESS");
                return new JsonResult(new
                {
                    Status = "SUCCESS"
                });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                _logger.LogInformation($"return  RETRY");
                return new JsonResult(new
                {
                    Status = "RETRY"
                });
            }
        }
    }


    public class PubBody
    {
        public string id { get; set; }
        public string source { get; set; }
        public string pubsubname { get; set; }
        public string traceid { get; set; }
        public PubOrder data { get; set; }
        public string specversion { get; set; }
        public string datacontenttype { get; set; }
        public string type { get; set; }
        public string topic { get; set; }
    }


    public class PubOrder
    {
        public string OrderNo { get; set; }
        public decimal Amount { get; set; }
        public DateTime OrderTime { get; set; }     
    }
}