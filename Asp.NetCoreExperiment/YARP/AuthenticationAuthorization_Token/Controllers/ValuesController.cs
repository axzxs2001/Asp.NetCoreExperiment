using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAuthorization_Token.Controllers
{
    [Authorize("Permission")]  
    [ApiController]
    public class ValuesController : ControllerBase
    {
  
        [HttpGet("/adminapi")]
        public ActionResult GetAdmin()
        {

            return new JsonResult(new { Name = "Admin" });
        }
        [HttpGet("/systemapi")]
        public ActionResult GetSystem()
        {
            return new JsonResult(new { Name = "System" });
        }
    }
}
