using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace EmbeddedResourcesPage.Controllers
{
    public class PageController : Controller
    {
        private readonly ILogger<PageController> _logger;

        public PageController(ILogger<PageController> logger)
        {
            _logger = logger;
        }
        [HttpGet("page")]
        public IActionResult Index()
        {
            _logger.LogInformation("----------------page/index");
            return View();
        }

    }
}
