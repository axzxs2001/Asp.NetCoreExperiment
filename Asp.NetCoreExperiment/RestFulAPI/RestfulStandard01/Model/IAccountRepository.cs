using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulStandard01.Model
{
    /// <summary>
    /// account仓储
    /// </summary>
    public interface IAccountRepository 
    {
        /// <summary>
        /// 按用户ID获取帐号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
         IList<Account> GetAccountsByUserID(int userId);

        /// <summary>
        /// 按用户ID，帐户ID获取帐户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accountId">帐户ID</param>
        /// <returns></returns>
        Account GetAccountByID(int userId, int accountId);

        /// <summary>
        /// 添加帐户
        /// </summary>
        /// <param name="account">帐户</param>
        /// <returns></returns>
        Account AddAccount(Account account);

        /// <summary>
        /// 添加帐户集合
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accounts">帐户集合</param>
        /// <returns></returns>
        IEnumerable<Account> AddAccounts(int userId, IEnumerable<Account> accounts);

        /// <summary>
        /// 按照ID获取帐户
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns></returns>
        IEnumerable<Account> GetAccounts(int[] ids);
    }
}
