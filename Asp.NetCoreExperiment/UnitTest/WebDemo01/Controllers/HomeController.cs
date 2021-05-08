using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo01.Services;

namespace WebDemo01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly IShopService _shopService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IShopService shopService)
        {
           
            _logger = logger;
            _shopService = shopService;

        }

        [HttpGet]
        public string Get()
        {
            _shopService.FF();
            return "OK";
        }
    }
}
