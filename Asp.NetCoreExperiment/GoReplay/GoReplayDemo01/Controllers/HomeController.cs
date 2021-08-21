using Microsoft.AspNetCore.Mvc;

namespace GoReplayDemo01.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/getvalue/{id}")]
    public async Task<string> Get(int id)
    {
        _logger.LogInformation($"X-NSS-UUID:{Request.Headers["X-NSS-UUID"]}");
        _logger.LogInformation($"{DateTime .Now}---{id.ToString()}");
     
        var client = new HttpClient();
        var content = await client.GetStringAsync("https://www.google.com");
        return $"{DateTime.Now.ToString()}-{id}-----------{content}";
    }
    [HttpPost("/pay")]
    public IActionResult Post([FromBody] Pay pay)
    {
        _logger.LogInformation($"X-NSS-UUID:{Request.Headers["X-NSS-UUID"]}");
        _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(pay));
        pay.Status = 200;
        return new JsonResult(pay);
    }


}

public class Pay
{
    public string Code { get; set; }
    public decimal Amount { get; set; }

    public int Status { get; set; }
}
