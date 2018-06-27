using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulStandard01.Model
{
    /// <summary>
    /// user仓储
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// 按ID获取用户
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        User GetUserByID(int id);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        User AddUser(User user);

        /// <summary>
        /// 返回分页数据
        /// </summary>
        /// <param name="userPagination"></param>
        /// <returns></returns>
        PaginatedList<User> GetPagingUser(UserPagination  userPagination);
    }
}
