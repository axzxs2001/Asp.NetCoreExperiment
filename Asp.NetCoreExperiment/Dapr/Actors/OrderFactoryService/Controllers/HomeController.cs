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

        [HttpGet("/abc")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}