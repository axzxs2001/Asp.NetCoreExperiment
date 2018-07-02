using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerAPI01.Controllers
{
    /// <summary>
    /// values controller
    /// </summary>
    [Authorize("permission")]
    [Route("api01/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Get方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<API01Model>), 200)]
        public ActionResult<IEnumerable<API01Model>> Get()
        {
            return new API01Model[] {
                new API01Model { ID=1, IsSure=true, Price=2.3m, Describe="test1" },
                new API01Model { ID=2, IsSure=true, Price=1.3m, Describe="test2" },
            };
        }

        /// <summary>
        /// Get带参方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(API01Model), 200)]
        public ActionResult<API01Model> Get(int id)
        {
            return new API01Model { ID = 1, IsSure = true, Price = 2.3m, Describe = "test1" };
        }

        /// <summary>
        /// Post方法
        /// </summary>
        /// <param name="api01Model"></param>
        [HttpPost]
        [ProducesResponseType(typeof(API01Model), 200)]
        public API01Model Post([FromBody] API01Model api01Model)
        {
            api01Model.ID = 100;
            return api01Model;
        }

        /// <summary>
        /// Put方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="api01Model"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        public bool Put(int id, [FromBody] API01Model api01Model)
        {
            return true;
        }
        /// <summary>
        /// Delete方法
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(typeof(int), 200)]
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return true;
        }
    }
}
