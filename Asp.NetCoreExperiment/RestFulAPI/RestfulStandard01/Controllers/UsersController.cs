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
    [Route("api/[controller]")]
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
        /// 按用户获取帐号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<Account>), 200)]
        [HttpGet("{userId}/accounts")]
        public ActionResult GetAccounts(int userId)
        {
            var accounts = _userRepository.GetAccountsByUserID(userId);
            if (accounts == null || accounts.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(accounts);
            }
        }
        /// <summary>
        /// 按用户ID和帐户ID查询帐户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accountId">帐户ID</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Account), 200)]
        [HttpGet("{userId}/accounts/{accountId}")]
        public IActionResult GetAccount(int userId, int accountId)
        {
            var account = _userRepository.GetAccountByID(userId, accountId);
            if (account == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(account);
            }
        }
    }

}
