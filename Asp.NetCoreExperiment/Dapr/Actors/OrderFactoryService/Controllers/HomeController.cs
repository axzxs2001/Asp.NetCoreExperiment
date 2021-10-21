using Microsoft.AspNetCore.Mvc;

namespace OrderFactoryService.Controllers
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

        [HttpGet("/gettime")]
        public IActionResult Get(string inTime)
        {
            Task.Delay(3000).Wait();
            return Ok($"传入时间：{inTime}，返回时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
        }
    }
}