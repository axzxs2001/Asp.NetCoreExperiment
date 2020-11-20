using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI02.Controllers
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

        [HttpGet("/webapi02/test2")]
        public string Get2()
        {
            _logger.LogInformation("WebAPI02.TestController.Get2");
            return "WebAPI02.TestController.Get2";
        }
        [HttpGet("/webapi02/test4")]
        public string Get4()
        {
            _logger.LogInformation("WebAPI02.TestController.Get4");
            return "WebAPI02.TestController.Get4";
        }
    }
}
