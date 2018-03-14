using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Demo002_WebAPITest.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            var begin = DateTime.Now;
            var client1 = new HttpClient();
            var result1 =  client1.GetStringAsync("http://localhost:5000/api/values/1");

            var client2 = new HttpClient();
            var result2 =  client2.GetStringAsync("http://localhost:5000/api/values/1");
            var result = (Convert.ToInt32(await result1) + Convert.ToInt32(await result2)).ToString();
            return (DateTime .Now-begin).TotalSeconds+"秒   "+ result;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
