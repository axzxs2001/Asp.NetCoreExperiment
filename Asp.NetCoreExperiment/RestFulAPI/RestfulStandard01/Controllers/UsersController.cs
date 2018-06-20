using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulStandard01.Model;

namespace RestfulStandard01.Controllers
{

    /// <summary>
    /// 用户Controller
    /// </summary>  
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        readonly ILogger<UsersController> _logger;
        /// <summary>
        /// 
        /// </summary>
        readonly IUserRepository _userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userRepository"></param>
        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// 获取用户
        /// 资源应该使用名词, 它是个东西, 不是动作.
        /// api/getusers 就是不正确的.
        /// GET api/users 就是正确的
        /// GET api/users/{userId}
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(User), 200)]
        [HttpGet("{id}")]
        [HttpHead("{id}")]
        [HttpOptions("{id}")]
        public ActionResult GetUser(int id)
        {
            var user = _userRepository.GetUserByID(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }

        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(User), 200)]
        [HttpPost]
        public ActionResult AddUser([FromBody]User user)
        {
            var backUser = _userRepository.AddUser(user);
            if (backUser == null)
            {
                return NotFound();
            }
            else
            {
                return CreatedAtAction("GetUser",new { id=backUser.ID},backUser);
            }
        }



    }

}
