using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulStandard01.Model
{
    /// <summary>
    /// user仓储
    /// </summary>
    public class AccountRepository : IAccountRepository
    {

        /// <summary>
        /// 按用户ID获取帐号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public IList<Account> GetAccountsByUserID(int userId)
        {
            return new List<Account>() {
                new Account { ID=1, AccountNo="123445", AccountType="QQ" }
            };
        }
        /// <summary>
        /// 按用户ID，帐户ID获取帐户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accountId">帐户ID</param>
        /// <returns></returns>
        public Account GetAccountByID(int userId, int accountId)
        {
            return new Account()
            {
                ID = 10,
                AccountNo = "12334567",
                AccountType = "QQ",
                UserID = 1
            };
        }
        /// <summary>
        /// 添加帐户
        /// </summary>
        /// <param name="account">帐户</param>
        /// <returns></returns>
        public Account AddAccount(Account account)
        {
            account.ID = new Random().Next(10, 500);
            return account;
        }
        /// <summary>
        /// 添加帐户集合
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accounts">帐户集合</param>
        /// <returns></returns>
        public IEnumerable<Account> AddAccounts(int userId, IEnumerable<Account> accounts)
        {
            var id = new Random().Next(10, 500);

            foreach (var account in accounts)
            {
                account.ID = id++;
                account.UserID = userId;
            }
            return accounts;
        }
        /// <summary>
        /// 按照ID获取帐户
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns></returns>
        public IEnumerable<Account> GetAccounts(int[] ids)
        {
            var accounts = new List<Account>();
            foreach (var id in ids)
            {
                accounts.Add(new Account { ID = id, AccountNo = "00000" + id, AccountType = "QQ", UserID = 12 });
            }
            return accounts;
        }
    }
}
