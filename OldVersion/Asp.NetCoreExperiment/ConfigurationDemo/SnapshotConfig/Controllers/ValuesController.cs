using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SnapshotConfig.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        AppSetting _appSetting;
        AppSetting _appSetting1;
       // public ValuesController(IOptionsSnapshot<AppSetting> AppSettingOpt, IOptionsMonitor<AppSetting> optionsAccessor)
         public ValuesController(IOptionsSnapshot<AppSetting> AppSettingOpt)
        {
            _appSetting = AppSettingOpt.Value;
            //_appSetting1 = optionsAccessor.CurrentValue;
            //optionsAccessor.OnChange((app, a) =>
            //{
            //    Console.WriteLine($"================{app.Key}");
            //    if (!string.IsNullOrEmpty(a))
            //    {
            //        Console.WriteLine($"----------------{a}");
            //    }
            //});
            //Console.WriteLine($"----------------{_appSetting1.Key}");
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", _appSetting.Key };
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

    public class AppSetting
    {
        public string Key { get; set; }
    }
}
