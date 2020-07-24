using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;

namespace InitiateHTTPClientDemo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly HttpClient _client;
        public ValuesController(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("polly");
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _client.BaseAddress = new System.Uri("http://www.baidu.com");
            var response = _client.GetAsync("").Result;
            var content = response.Content.ReadAsStringAsync().Result;

            return new string[] { "value1", content };
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
}
