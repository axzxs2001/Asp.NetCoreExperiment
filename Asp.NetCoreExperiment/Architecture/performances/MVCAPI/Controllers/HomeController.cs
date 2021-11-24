using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace MVCAPI.Controllers
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

        [HttpGet("/test01")]
        public string Test01()
        {
            var arr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            var index = RandomNumberGenerator.GetInt32(arr.Length);
            return arr[index];
        }
    }
}