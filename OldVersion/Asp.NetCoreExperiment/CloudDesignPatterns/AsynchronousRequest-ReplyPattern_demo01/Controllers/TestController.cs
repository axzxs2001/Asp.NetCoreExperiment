using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AsynchronousRequest_ReplyPattern_demo01.Controllers
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


        [HttpPost("/pay")]
        public IActionResult Post(string orderNo,int quantity)
        {
            var id = Guid.NewGuid().ToString();
            Response.StatusCode = 202;
            Response.Headers.Add("Location", "/payresultstatu");
            Response.Headers.Add("Retry-Afte", "2000");
            return StatusCode(202, id);
        }
        [HttpGet("/payresultstatu")]
        public IActionResult GetResultStatus(string id)
        {
            var random = new Random();
            var value = random.Next(1, 5);
            if (value % 2 == 0)
            {
                return StatusCode(202, id);
            }
            else
            {
                return Redirect($"/payresult?id={id}");
            }
        }

        [HttpGet("/payresult")]
        public IActionResult GetResult(string id)
        {
            return Ok($"完成{id}");
        }
    }
}
