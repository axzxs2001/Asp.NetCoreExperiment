using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PaymentSystem.Controllers;
[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/pay")]
    public async Task<IActionResult> TestGet()
    {
        _logger.LogInformation($"开始支付");
        await Task.Delay(200);
        _logger.LogInformation($"支付完成");
        return new JsonResult(new { result = true, message = "支付成功", host = Dns.GetHostName() });
    }
}
