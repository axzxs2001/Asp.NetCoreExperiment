using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API01.Controllers
{
    [Authorize(Policy = "Permission")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet("/abc")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "4567" };
        }

    }
}
