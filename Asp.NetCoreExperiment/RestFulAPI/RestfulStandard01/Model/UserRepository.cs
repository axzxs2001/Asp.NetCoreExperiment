using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulStandard01.Model
{
    /// <summary>
    /// user仓储
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// 按照ID获取用户
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public User GetUserByID(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            else
            {
                var user = new User
                {
                    ID = 1,
                    UserName = "gsw",
                    Password = "1111111"
                };
                return user;
            }
        }
    }
}
