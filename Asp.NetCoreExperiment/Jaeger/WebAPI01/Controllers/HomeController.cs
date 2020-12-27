using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAPI01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            _logger.LogInformation("WebAPI01中请求WebAPI02");
            var result = await GetWebAPI02();
            return $"WebAPI01请求WebAPI02返回值 :{ result}";
        }

        async Task<string> GetWebAPI02()
        {
            using var client = _clientFactory.CreateClient("WebAPI02");
            var request = new HttpRequestMessage(HttpMethod.Get, "/home");
            using var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                return "error";
            }
        }
    }
}