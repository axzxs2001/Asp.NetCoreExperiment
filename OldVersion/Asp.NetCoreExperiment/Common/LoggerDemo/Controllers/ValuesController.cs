using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace LoggerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly ILogger<ValuesController> _logger;
        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }




        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //批处理日志
            using (_logger.LogoInfo3(123))
            {
                _logger.LogInfo1();
                _logger.LogInfo2(DateTime.Now.ToString());

            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    static class LogExt
    {
        private static readonly Action<ILogger, Exception> _logInfo1 = LoggerMessage.Define(
   LogLevel.Information, new EventId(10001, nameof(ValuesController)), "-----------1--------GET request for Index page");

        public static void LogInfo1(this ILogger logger)
        {
            _logInfo1(logger, null);
        }

        private static readonly Action<ILogger, string, Exception> _logInfo2 = LoggerMessage.Define<string>(
LogLevel.Information, new EventId(10002, nameof(ValuesController)), "----------2--------GET request for Index page {par}");
        public static void LogInfo2(this ILogger logger, string par)
        {
            _logInfo2(logger, par, null);
        }

        private static Func<ILogger, int, IDisposable> _logInfo3 = LoggerMessage.DefineScope<int>("----------3------All quotes deleted (Count = {Count})");
        public static IDisposable LogoInfo3(this ILogger logger, int count)
        {
            return _logInfo3(logger, count);
        }
    }
}
