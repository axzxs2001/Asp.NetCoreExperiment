using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServerDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/person")]
        public IActionResult GetPerson(Person person)
        {
            _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(person));
            return new JsonResult(person);
        }
    }
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
 
}
