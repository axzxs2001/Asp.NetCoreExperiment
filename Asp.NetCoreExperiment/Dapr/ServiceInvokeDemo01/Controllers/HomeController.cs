using Microsoft.AspNetCore.Mvc;

namespace ServiceInvokeDemo01.Controllers;
[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _clientFactory;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory _clientFactory)
    {
        _logger = logger;
        this._clientFactory = _clientFactory;
    }

    [HttpGet("/test")]
    public async Task<IActionResult> TestGet()
    {
        _logger.LogInformation($"app2:6000  调用 {DateTime.Now}");
        //var client = _clientFactory.CreateClient();
        //var content = await client.GetStringAsync("http://localhost:3500/v1.0/invoke/app1/method/test");
        var content = "aaaa";
        return new JsonResult(new { port = "6000", content });
    }


}
