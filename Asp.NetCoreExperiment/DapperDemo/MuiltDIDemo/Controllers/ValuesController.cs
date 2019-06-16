using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MuiltDIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {     

        private IJK _serviceA;
        private IJK _serviceB;
        private readonly Func<string, IJK> _serviceAccessor;

        //第一种方式
        //public ValuesController(Func<string, IJK> serviceAccessor)
        //{
        //    this._serviceAccessor = serviceAccessor;
        //    _serviceA = _serviceAccessor("JK1");
        //    _serviceB = _serviceAccessor("JK2");

        //}  
        
         //第二种方式
        public ValuesController(IEnumerable<IJK> svs)
        {
            foreach(var sv in svs)
            {
                if(sv is JK1)
                {
                    _serviceA = sv;
                }
                if (sv is JK2)
                {
                    _serviceB = sv;
                }
            }
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
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
}



 






