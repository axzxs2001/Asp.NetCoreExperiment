using Microsoft.Extensions.Logging;
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
        readonly ILogger<UserRepository> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UserRepository(ILogger<UserRepository> logger)
        {
            _logger = logger;
        }

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
        /// <param name="userPagination">分页</param>
        /// <returns></returns>
        public PaginatedList<User> GetPagingUser(UserPagination userPagination)
        {            
            var users = new List<User>();
            for (int i = 1; i < 105; i++)
            {
                users.Add(new User { ID = i, Name = "user" + i, Password = "111111", UserName = "username" + i ,UserType=i%3});
            }
            var pageinatedList = new PaginatedList<User>(userPagination.PageIndex, userPagination.PageSize, users.Count, users.Skip(userPagination.PageIndex * userPagination.PageSize).Take(userPagination.PageSize));
            return pageinatedList;
        }
    }
}
