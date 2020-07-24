using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Text;

namespace DataProtectionDemo002.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IDataProtectionProvider _provider;
        IDataProtector _dataProtector;
        ITimeLimitedDataProtector _timeLimitedDataProtector;
        IKeyManager _keyManager;
        public ValuesController(IDataProtectionProvider provider, IKeyManager keyManager)
        {
            _provider = provider;
            _dataProtector = provider.CreateProtector("3BCE558E2AD3E0E34A7743EAB5AEA2A9BD2575A0");
            _timeLimitedDataProtector = _dataProtector.ToTimeLimitedDataProtector();
            _keyManager = keyManager;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
           // var endstring = _dataProtector.Protect("桂素伟");
            var endstring = _timeLimitedDataProtector.Protect("桂素伟", TimeSpan.FromSeconds(54000));
            return endstring;
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

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            var endstring = _dataProtector.Protect(Encoding.UTF8.GetBytes("桂素伟"));
            IPersistedDataProtector persistedProtector = _dataProtector as IPersistedDataProtector;
            if (persistedProtector == null)
            {
                throw new Exception("Can't call DangerousUnprotect.");
            }
            bool requiresMigration, wasRevoked;
            var unprotectedPayload = persistedProtector.DangerousUnprotect(
                protectedData: endstring,
                ignoreRevocationErrors: true,
                requiresMigration: out requiresMigration,
                wasRevoked: out wasRevoked);
            var str = $"Unprotected payload: {Encoding.UTF8.GetString(unprotectedPayload)},Requires migration = {requiresMigration}, was revoked = {wasRevoked}";
        
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            try
            {
                _keyManager.RevokeAllKeys(DateTimeOffset.Now, "Sample revocation.");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
