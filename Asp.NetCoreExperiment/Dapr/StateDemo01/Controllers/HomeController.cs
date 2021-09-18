using Microsoft.AspNetCore.Mvc;

namespace StateDemo01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _stateUrl;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _stateUrl = configuration.GetSection("StateUrl").Value;
            _clientFactory = clientFactory;
            _logger = logger;
        }

        [HttpPost("/writekeys")]
        public async Task<IActionResult> WriteKeys([FromBody] KeyEntity[] keys)
        {
            var client = _clientFactory.CreateClient();
            var jsonContent = System.Text.Json.JsonSerializer.Serialize(keys);
            var content = new StringContent(jsonContent);
            var response = await client.PostAsync(_stateUrl, content);
            return Ok(await response.Content.ReadAsStringAsync());
        }

        [HttpGet("/readekey/{key}")]
        public async Task<IActionResult> ReadKey(string key)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{_stateUrl}/{key}");
            return Ok(await response.Content.ReadAsStringAsync());
        }
        [HttpPost("/readekeys")]
        public async Task<IActionResult> ReadKeys([FromBody] string[] keys)
        {
            var client = _clientFactory.CreateClient();
            var jsonContent = System.Text.Json.JsonSerializer.Serialize(keys);
            var content = new StringContent(jsonContent);
            var response = await client.PostAsync($"{_stateUrl}/bulk", content);
            return Ok(await response.Content.ReadAsStringAsync());
        }

        [HttpDelete("/deletekey/{key}")]
        public async Task<IActionResult> DeleteData(string key)
        {  
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_stateUrl}/{key}");
            return Ok(await response.Content.ReadAsStringAsync());
        }     
    }

    public class KeyEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}