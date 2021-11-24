using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationWebDemo01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IValidator<Person> _validator;

        public HomeController(ILogger<HomeController> logger, IValidator<Person> validator)
        {
            _validator = validator;
            _logger = logger;
        }

        [HttpPost("/addperson")]
        public IActionResult AddPerson([FromBody] Person person)
        {
            _logger.LogInformation("添加Person");
            if (ModelState.IsValid)
            {
                return Ok("验证成功后，假装这里作了后端业务处理");
            }
            _logger.LogError("验证Person失败");
            return BadRequest("person没有验证通过");
        }
        [HttpPost("/addperson2")]
        public IActionResult AddPerson2([FromBody] Person person)
        {
            _logger.LogInformation("添加Person");
            if (ModelState.IsValid)
            {
                return Ok("验证成功后，假装这里作了后端业务处理");
            }
            _logger.LogError("验证Person失败");
            return BadRequest("person没有验证通过");
        }
    }
}
