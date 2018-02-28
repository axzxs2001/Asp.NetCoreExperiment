using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API001.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        [HttpGet]
        public string Get()
        {
            return $"API001:{DateTime.Now.ToString()}";
        }

        [HttpGet("/health")]
        public IActionResult Heathle()
        {
            return Ok();
        }
    }
}
