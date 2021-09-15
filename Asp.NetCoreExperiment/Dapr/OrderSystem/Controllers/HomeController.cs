using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderSystem.Controllers;
[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _clientFactory;
    private readonly string? _payUrl;
    public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _payUrl = configuration.GetSection("payurl").Value;
        _clientFactory = clientFactory;
        _logger = logger;
    }
    [HttpGet("/order")]
    public async Task<IActionResult> Order()
    {
        _logger.LogInformation($"下单开始");
        await Task.Delay(400);
        _logger.LogInformation($"订单完成   调用支付系统");
        var client = _clientFactory.CreateClient();
        var content = await client.GetStringAsync(_payUrl);
        return new JsonResult(new { pay_result = content });
    }
}
