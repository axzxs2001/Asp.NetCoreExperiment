using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
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

        [HttpGet("/api001")]
        public async Task<IActionResult> GetAPI001()
        {
            await Task.Delay(1000);
            return new JsonResult(new { result = true, data = "api001 返回成功" });
        }
        [HttpGet("/api002")]
        public async Task<IActionResult> GetAPI002()
        {
            await Task.Delay(1000);
            return new JsonResult(new { result = true, data = "api002 返回成功" });
        }
        [HttpGet("/api003")]
        public async Task<IActionResult> GetAPI003()
        {
            await Task.Delay(1000);
            return new JsonResult(new { result = true, data = "api003 返回成功" });
        }
    }
}
