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

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("WebAPI01.TestController.Get");
            return "WebAPI01.TestController.Get";
        }

    }
}
