using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PollyDBConnectionDemo.Services;

namespace PollyDBConnectionDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly AdoNetPolly _adoNetPolly;
        readonly DapperPolly _dapperPolly;
        readonly ReliableDapper _reliableDapper;
        public ValuesController(AdoNetPolly adoNetPolly, DapperPolly dapperPolly, ReliableDapper reliableDapper)
        {
            _dapperPolly = dapperPolly;
            _adoNetPolly = adoNetPolly;
            _reliableDapper = reliableDapper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var result = 1;// _dapperPolly.GetCount();
            return new string[] { "value1", result.ToString() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var list = _reliableDapper.Query<string>("select id from test1").ToList();
            return list[0];
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
