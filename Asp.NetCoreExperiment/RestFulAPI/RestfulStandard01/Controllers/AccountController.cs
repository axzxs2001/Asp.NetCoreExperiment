using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    [Route("api/users/{userId}/accounts/")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        readonly ILogger<AccountsController> _logger;
        /// <summary>
        /// 
        /// </summary>
        readonly IAccountRepository _accountRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountRepository"></param>
        public AccountsController(ILogger<AccountsController> logger, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }


        /// <summary>
        /// 按用户获取帐号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<Account>), 200)]
        [HttpGet]
        public ActionResult GetAccounts(int userId)
        {
            var accounts = _accountRepository.GetAccountsByUserID(userId);
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
        [HttpGet("{accountId}")]
        public IActionResult GetAccount(int userId, int accountId)
        {
            var account = _accountRepository.GetAccountByID(userId, accountId);
            if (account == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(account);
            }
        }
        /// <summary>
        /// 添加帐户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="account">帐户</param>
        /// <returns></returns>         
        [ProducesResponseType(typeof(Account), 200)]
        [HttpPost]
        public IActionResult AddAccount(int userId, [FromBody]Account account)
        {
            account.UserID = userId;
            var backAccount = _accountRepository.AddAccount(account);
            if (backAccount == null)
            {
                return NotFound();
            }
            else
            {
                return CreatedAtAction("GetAccount", new { userId = backAccount.UserID, accountId = backAccount.ID }, backAccount);
            }
        }

        /// <summary>
        /// 批量添加帐户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accounts">批量帐户</param>
        /// <returns></returns>         
        [ProducesResponseType(typeof(IEnumerable<Account>), 200)]
        [HttpPost("batch")]
        public IActionResult AddAccount(int userId, [FromBody]IEnumerable<Account> accounts)
        {
            var backAccounts = _accountRepository.AddAccounts(userId, accounts);
            if (backAccounts == null && backAccounts.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return CreatedAtAction("GetAccounts", new { ids = string.Join(',', backAccounts.Select(s => s.ID)) }, backAccounts);
            }
        }
        /// <summary>
        /// 按照ids获取帐户
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<Account>), 200)]
        [HttpGet("({ids})")]
        public IActionResult GetAccounts(string ids)
        {
            var idarr = ids.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToArray();
            var accounts = _accountRepository.GetAccounts(idarr);
            if (accounts == null && accounts.Count() > 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(accounts);
            }
        }
        /// <summary>
        /// 删除帐户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accountId">帐户ID</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(int), 200)]
        [HttpDelete("{accountId}")]
        public IActionResult DeleteAccount(int userId, int accountId)
        {
            if (userId == 1 && accountId == 1)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        /// <summary>
        /// 修改帐户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accountId">帐户ID</param>
        /// <param name="account">帐户</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(int), 200)]
        [HttpPut("{accountId}")]
        public IActionResult UpdateAccount(int userId, int accountId, [FromBody]Account account)
        {
            if (string.IsNullOrEmpty(account.AccountNo) || string.IsNullOrEmpty(account.AccountType))
            {
                return BadRequest();
            }
            if (userId == 1 && accountId == 1)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }


        
    }

}
