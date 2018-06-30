using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuartzNetDemo4.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace QuartzNetDemo4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IBackgroundRepository _backgroundRepository;
        public ValuesController(IBackgroundRepository backgroundRepository)
        {
            _backgroundRepository = backgroundRepository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
         
            return new string[] { "value1", "value1"};
        }

        // GET api/values/5
        [HttpGet("{date}")]
        public ActionResult<string> Get(string date)
        {
            _backgroundRepository.StarPayDailyReport(DateTime.Parse(date));
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
