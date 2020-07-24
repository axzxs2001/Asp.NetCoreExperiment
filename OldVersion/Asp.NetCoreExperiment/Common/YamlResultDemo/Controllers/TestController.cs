using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace YamlResultDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new YamlResult(new
            {
                Data = new { a = 1, B = "bbb", C = DateTime.Now, d = new string[] { "a", "b", "c" } },
                Status = true,
                Message = "成功"
            });
        }
    }
 
}
