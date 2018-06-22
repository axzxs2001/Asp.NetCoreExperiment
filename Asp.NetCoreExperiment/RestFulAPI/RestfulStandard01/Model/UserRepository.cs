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

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public User AddUser(User user)
        {
            user.ID = new Random().Next(10, 500);
            return user;
        }

        /// <summary>
        /// 返回分页数据
        /// </summary>
        /// <param name="paginationBase">分页</param>
        /// <returns></returns>
        public PaginatedList<User> GetPagingUser(PaginationBase paginationBase)
        {
            var users = new List<User>();
            for (int i = 1; i < 105; i++)
            {
                users.Add(new User { ID = i, Name = "user" + i, Password = "111111", UserName = "username" + i });
            }
            var pageinatedList = new PaginatedList<User>(paginationBase.PageIndex, paginationBase.PageSize, users.Count, users.Skip(paginationBase.PageIndex * paginationBase.PageSize).Take(paginationBase.PageSize));
            return pageinatedList;
        }
    }
}
