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
    }
}
