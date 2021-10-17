using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    private readonly string _stateUrl;
    private readonly string _publishUrl;
    private readonly string _secretUrl;
    public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _stateUrl = configuration.GetSection("StateUrl").Value;
        _payUrl = configuration.GetSection("payurl").Value;
        _publishUrl = configuration.GetSection("PublishUrl").Value;
        _secretUrl = configuration.GetSection("SecretUrl").Value;
        _clientFactory = clientFactory;
        _logger = logger;
    }

    [HttpGet("/orderpub/{orderno}")]
    public async Task<IActionResult> OrderPub(string orderno)
    {
        try
        {
            _logger.LogInformation($"Order,publish");
            await Task.Delay(400);
            var client = _clientFactory.CreateClient();

            var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderNo = orderno, Amount = 30000, OrderTime = DateTime.UtcNow }), System.Text.Encoding.UTF8, "application/json");
            _logger.LogInformation(stringContent.ToString());
            var response = await client.PostAsync(_publishUrl, stringContent);


            IEnumerable<string>? values1, values2;
            string traceparentValue = "";
            string tracestateValue = "";
            if (response.Headers.TryGetValues("traceparent", out values1))
            {
                traceparentValue = values1.FirstOrDefault();
            }
            if (response.Headers.TryGetValues("tracestate", out values2))
            {
                tracestateValue = values2.FirstOrDefault();
            }
            var content = await response.Content.ReadAsStringAsync();
            return new JsonResult(new { order_result = "Order success,and publish", pay_result = content, traceparentValue, tracestateValue });
        }
        catch (Exception exc)
        {
            _logger.LogCritical(exc, exc.Message);
            return new JsonResult(new { order_result = "Order success,and publish,pay exception", message = exc.Message });
        }
    }


    [HttpGet("/order")]
    public async Task<IActionResult> Order()
    {
        try
        {
            _logger.LogInformation($"下单开始");
            await Task.Delay(400);
            _logger.LogInformation($"订单完成   调用支付系统");
            var client = _clientFactory.CreateClient();
            //var content = await client.GetStringAsync(_payUrl);

            var request = new HttpRequestMessage(HttpMethod.Get, _payUrl);
            var response = await client.SendAsync(request);


            IEnumerable<string>? values1, values2;
            string traceparentValue = "";
            string tracestateValue = "";
            if (response.Headers.TryGetValues("traceparent", out values1))
            {
                traceparentValue = values1.FirstOrDefault();
            }
            if (response.Headers.TryGetValues("tracestate", out values2))
            {
                tracestateValue = values2.FirstOrDefault();
            }

            var content = await response.Content.ReadAsStringAsync();
            return new JsonResult(new { order_result = "订单成功", pay_result = content, traceparentValue, tracestateValue });
        }
        catch (Exception exc)
        {
            _logger.LogCritical(exc, exc.Message);
            return new JsonResult(new { order_result = "订单成功，支付失败", message = exc.Message });
        }
    }


    [HttpGet("/queryappkey")]
    public async Task<IActionResult> QueryAppkey()
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync(_secretUrl);
        var dataString = await response.Content.ReadAsStringAsync();
        return new JsonResult(new { result = true, appkey = dataString, host = Dns.GetHostName() });
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
        var dataString = await response.Content.ReadAsStringAsync();
        return new JsonResult(new { result = true, data = dataString, host = Dns.GetHostName() });
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
    public OrderPayment Value { get; set; }
}

public class OrderPayment
{
    public string PayOrder { get; set; }

    public decimal PayTotal { get; set; }
    public string PayType { get; set; }

    public DateTime PayTime { get; set; }
}