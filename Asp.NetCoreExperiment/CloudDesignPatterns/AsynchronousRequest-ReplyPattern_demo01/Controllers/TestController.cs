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


        [HttpGet("/pay")]
        public IActionResult Post()
        {
            Response.StatusCode = 202;
            Response.Headers.Add("Location", "/payresult");
            Response.Headers.Add("Retry-Afte", "3");
            return StatusCode(202);
        }
        [HttpGet("/payresult")]
        public IActionResult GetResult()
        {
            return Ok("完成");
        }
    }
}
