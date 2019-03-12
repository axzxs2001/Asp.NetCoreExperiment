using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;

namespace API02.Controllers
{
    [Authorize(Policy = "Permission")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("/abc")]
        public ActionResult<IEnumerable<string>> Get([FromQuery]string par)
        {
            var claimsStr = new StringBuilder();
            claimsStr.Append($"QueryString参数 par = {par};");
            claimsStr.Append("    Claim值:");
            foreach (var claim in User.Claims)
            {             
                claimsStr.Append($"{claim.Type} : {claim.Value} , ");
            }
            return new string[] { "4568", claimsStr.ToString() };
        }
    }
}
