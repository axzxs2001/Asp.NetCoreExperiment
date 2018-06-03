using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestAPIDemo01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersionController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult GetPersion(int id)
        {
            var list = new List<Person>() {
                new Person {ID=1,Name="1111"},
                new Person {ID=2,Name="2222"},
                new Person {ID=3,Name="3333"},
                new Person {ID=4,Name="4444"},
            };
            return Ok(list.SingleOrDefault(s=>s.ID==id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            person.ID = 4;
            return  CreatedAtAction("GetPersion", new { id = 4 },person);
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
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
