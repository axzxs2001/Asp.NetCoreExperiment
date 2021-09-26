using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Linq;

namespace PaymentSystem.Controllers;
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

    [HttpGet("/pay")]
    public async Task<IActionResult> TestGet()
    {
        _logger.LogInformation($"开始支付");
        await Task.Delay(200);
        _logger.LogInformation($"支付完成");
        return new JsonResult(new { result = true, message = "支付成功", host = Dns.GetHostName() });
    }
    #region state
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
        var dataString = await response.Content.ReadAsStringAsync();
        return new JsonResult(new { result = true, data = (string.IsNullOrEmpty(dataString) ? null : System.Text.Json.JsonSerializer.Deserialize<OrderPayment>(dataString)), host = Dns.GetHostName() });
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
    #endregion

    #region etag
    [HttpPost("/writekeyswithetag")]
    public async Task<IActionResult> WriteKeysWithEtag([FromBody] KeyEntityWithEtag[] keys)
    {
        var client = _clientFactory.CreateClient();
        var jsonContent = System.Text.Json.JsonSerializer.Serialize(keys);
        _logger.LogInformation("-------------------------------------------");
        _logger.LogInformation(jsonContent);
        var content = new StringContent(jsonContent);
        var response = await client.PostAsync(_stateUrl, content);
        return Ok(await response.Content.ReadAsStringAsync());
    }

    [HttpGet("/readekeywithetag/{key}")]
    public async Task<IActionResult> ReadKeyWithEtag(string key)
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync($"{_stateUrl}/{key}");
        _logger.LogInformation("=============================================");
        _logger.LogInformation("readekeywithetag headers:");
        foreach (var head in response.Headers)
        {
            _logger.LogInformation($"{head.Key}:{head.Value}");
        }
        var dataString = await response.Content.ReadAsStringAsync();
        var data = (string.IsNullOrEmpty(dataString) ? null : System.Text.Json.JsonSerializer.Deserialize<OrderPayment>(dataString));
        return new JsonResult(new { result = true, data = new { data = data, etag = response.Headers.SingleOrDefault(s => s.Key.ToLower() == "etag") }, host = Dns.GetHostName() });
    }
    #endregion
}

public class KeyEntity
{
    public string Key { get; set; }
    public OrderPayment Value { get; set; }
}

public class OrderPayment
{
    public string PayOrder { get; set; }
    public decimal PayTotal { get; set; }
    public string PayType { get; set; }
    public DateTime PayTime { get; set; }
}

public class KeyEntityWithEtag
{
    public string Key { get; set; }
    public OrderPayment Value { get; set; }
    public string Etag { get; set; }
}