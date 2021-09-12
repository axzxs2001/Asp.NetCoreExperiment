using Microsoft.AspNetCore.Mvc;

namespace ServiceInvokeDemo.Controllers;
[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/test")]
    public IActionResult TestGet()
    {
        _logger.LogInformation($"app1:5000  调用  {DateTime.Now}");
        return new JsonResult(new { time = DateTime.Now, port = "5000", result = true, message = "这是一个测试" });
    }

}
