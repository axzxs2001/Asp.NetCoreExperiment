using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Retry;

namespace PollyWebDemo.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {


        public readonly RetryPolicy<HttpResponseMessage> _httpRequestPolicy;
        public ValuesController()
        {
            _httpRequestPolicy = Policy.HandleResult<HttpResponseMessage>(r =>
            {
              
                return r.StatusCode != System.Net.HttpStatusCode.OK;
            }).WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{s}")]
        public string Get(string s)
        {
            if(s!="a")
            {
                this.HttpContext.Response.StatusCode = 500;
               return "is not  a,is exception";
            }
            else
            {
                return "Is a";
            }
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        public async  Task<string> Get(int id)
        {
            try
            {
                int i = 1;
                var httpClient = new HttpClient();
                HttpResponseMessage httpResponse =await _httpRequestPolicy.ExecuteAsync(() =>
                {
                    var url = $"http://localhost:50739/api/values/{(id == 1 ? "a" : "b")}";
                    Console.WriteLine($"========={i++}------{url}=========");
                   return httpClient.GetAsync(url);

                });

                var result =await httpResponse.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
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
