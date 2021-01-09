using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Person GetPerson()
        {
            _logger.LogInformation("请求person");
            return new Person() { ID = 100, Name = "张三丰收", Age = 22, Sex = true };
        }
    }

    public class Person
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public bool Sex { get; set; }
    }
}
