using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace CacheDemo02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IDistributedCache _cache;
        public ValuesController(IDistributedCache cache)
        {
            _cache = cache;
        }
    
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "gsw", _cache.GetString("gsw") };
        }


        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }


        [HttpPost]
        public void Post([FromBody]User user)
        {
            _cache.SetString("gsw", Newtonsoft.Json.JsonConvert.SerializeObject(user),new DistributedCacheEntryOptions {
                 SlidingExpiration=TimeSpan.FromSeconds(10)                  
            });
        }

  
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

     
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _cache.Remove(id);
        }
    }

    public class  User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
    }
}
