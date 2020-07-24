using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerAPI02.Controllers
{
    /// <summary>
    /// api01测试Values
    /// </summary>
    [Authorize("permission")]
    [Route("api02/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        /// <summary>
        /// Get方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<API02Model>), 200)]
        public ActionResult<IEnumerable<API02Model>> Get()
        {
            return new API02Model[] {
                new API02Model { ID=1, IsSure=true, Price=2.3m, Describe="test1" },
                new API02Model { ID=2, IsSure=true, Price=1.3m, Describe="test2" },
            };
        }

        /// <summary>
        /// Get带参方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(API02Model), 200)]
        public ActionResult<API02Model> Get(int id)
        {
            return new API02Model { ID = 1, IsSure = true, Price = 2.3m, Describe = "test1" };
        }

        /// <summary>
        /// Post方法
        /// </summary>
        /// <param name="api02Model"></param>
        [HttpPost]
        [ProducesResponseType(typeof(API02Model), 200)]
        public API02Model Post([FromBody] API02Model api02Model)
        {
            api02Model.ID = 100;
            return api02Model;
        }

        /// <summary>
        /// Put方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="api02Model"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        public bool Put(int id, [FromBody] API02Model api02Model)
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
