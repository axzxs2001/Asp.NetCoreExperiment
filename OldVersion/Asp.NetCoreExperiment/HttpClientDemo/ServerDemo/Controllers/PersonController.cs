using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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


        [HttpPost("/postperson2")]
        public IActionResult PostPerson2([FromBody]Person person)
        {
            _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(person));
            return new JsonResult(person);
        }


        [HttpPost("/postperson1")]
        public IActionResult PostPerson1([FromForm]Person person)
        {
            _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(person));
            return new JsonResult(person);
        }

        [HttpGet("/getperson3")]
        public IActionResult GetPerson3([FromQuery]Person person)
        {
            _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(person));
            return new JsonResult(person);
        }
        [HttpGet("/getperson2")]
        public IActionResult GetPerson2(Person person)
        {
            _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(person));
            return new JsonResult(person);
        }

        [HttpGet("/getperson1")]
        public IActionResult GetPerson1([FromBody]Person person)
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
