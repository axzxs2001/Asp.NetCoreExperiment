using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CustomeResultDemo2._2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new APIResult<dynamic>(new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
            })
            {
                Data = new { a = 1, B = "bbb" },
                Status = true,
                Message = "成功"
            };
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

    public class APIResult<T> : ActionResult
    {
        public bool Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        private JsonSerializerSettings _serializerSettings { get; set; }

        public APIResult()
        {
            Status = true;
        }
        public APIResult(JsonSerializerSettings serializerSettings)
        {
            Status = true;
            _serializerSettings = serializerSettings;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var services = context.HttpContext.RequestServices;
            var executor = services.GetRequiredService<JsonResultExecutor>();
            return executor.ExecuteAsync(context, _serializerSettings == null ? new JsonResult(this) : new JsonResult(this, _serializerSettings));
        }

    }
}
