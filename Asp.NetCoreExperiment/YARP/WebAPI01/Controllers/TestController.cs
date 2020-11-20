using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI01.Controllers
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

        [HttpGet("/webapi01/test1")]
        public string Get1()
        {
            _logger.LogInformation("WebAPI01.TestController.Get1");
            return "WebAPI01.TestController.Get1";
        }
        [HttpGet("/webapi01/test3")]
        public string Get3()
        {
            _logger.LogInformation("WebAPI01.TestController.Get3");
            return "WebAPI01.TestController.Get3";
        }

    }
}
