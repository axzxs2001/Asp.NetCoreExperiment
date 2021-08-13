using Exceptionless;
using Exceptionless.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FluentdWebDemo01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private readonly Dictionary<string, ExceptionlessClient> _clients;
        public HomeController(ILogger<HomeController> logger, Dictionary<string, ExceptionlessClient> clients)
        {
            _clients = clients;
            _logger = logger;
        }

        [HttpPost("/addlog")]
        public IActionResult AddFluendLog()
        {
            var result = Request.Headers.TryGetValue("appname", out Microsoft.Extensions.Primitives.StringValues appnames);
            var appname = appnames.ToString();
            var client = _clients[appname.ToLower()];
            if (client != null)
            {

                var b = new byte[(int)Request.ContentLength.Value];
                var r = Request.Body.ReadAsync(b, 0, b.Length).Result;
                var s = System.Text.Encoding.UTF8.GetString(b);
                var logbodys = System.Text.Json.JsonSerializer.Deserialize<LogBody[]>(s, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (DateTime.Now.Second % 3 == 0)
                {
                    client.SubmitException(new Exception($"异常：{s }"));
                }
                else
                {
                    foreach (var logbody in logbodys)
                    {
                        _logger.LogInformation(logbody.Message);
                        client.SubmitLog(appname, logbody.Message);

                    }
                }
            }
            return Ok();
        }
    }
    public class LogBody
    {
        public string Message { get; set; }
    }
}
