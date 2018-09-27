using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace DataProtectionDemo003.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IDataProtectionProvider _provider;
        IDataProtector _dataProtector;
        ITimeLimitedDataProtector _timeLimitedDataProtector;

        public ValuesController(IDataProtectionProvider provider)
        {
            _provider = provider;
            _dataProtector = provider.CreateProtector("3BCE558E2AD3E0E34A7743EAB5AEA2A9BD2575A0");
            _timeLimitedDataProtector = _dataProtector.ToTimeLimitedDataProtector();
          
        }

 
        // GET api/values/5
        [HttpGet("{endstring}")]
        public ActionResult<string> Get(string endstring)
        {
            try
            {
                //return _dataProtector.Unprotect(endstring);
                DateTimeOffset dateTimeOffset;
                var result = _timeLimitedDataProtector.Unprotect(endstring, out dateTimeOffset);
                return $"{result},{dateTimeOffset.LocalDateTime}";

            }
            catch (CryptographicException exc)
            {
                return $"{exc.Message},当前时间:{DateTime.Now}";
            }
        }

   


    
    }
}
