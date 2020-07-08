using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GraphQLDemo01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ISchema _schema;
        public TestController(ISchema schema, ILogger<TestController> logger)
        {
            _schema = schema;
            _logger = logger;
        }

        [HttpGet]
        public string Get(string cod=null)
        {
            cod = "{persons{id name}}";//"{person(id:1){id name}}";
            var executor = _schema.MakeExecutable();
            return executor.Execute(cod).ToJson();
        }
    }
    public class Query
    {     
        public IEnumerable<Person> GetPerson(int id)
        {
            return (new Person[] { new Person { Id = 1, Name = "AAA" }, new Person { Id = 2, Name = "BBBB" } }).Where(s=>s.Id==id);
        }
        public IEnumerable<Person> GetPersons()
        {
            return new Person[] { new Person { Id = 1, Name = "AAA" }, new Person { Id = 2, Name = "BBBB" } };
        }
    }
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
