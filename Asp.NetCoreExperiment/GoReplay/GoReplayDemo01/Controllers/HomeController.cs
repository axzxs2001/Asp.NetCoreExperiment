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
    [HttpPost("/order")]
    public IActionResult Post([FromBody] Order order)
    {
        _logger.LogInformation($"X-NSS-UUID:{Request.Headers["X-NSS-UUID"]}");
        _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(order));
        order.Status = 200;
        return new JsonResult(order);
    }


}

public class Order
{
    public string Code { get; set; }
    public decimal Amount { get; set; }

    public int Status { get; set; }
}
