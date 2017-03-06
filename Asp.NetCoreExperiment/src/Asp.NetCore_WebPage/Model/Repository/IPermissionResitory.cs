using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model.Repository
{
    /// <summary>
    /// 权限管理接口
    /// </summary>
    public interface IPermissionResitory
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool AddUser(User user);
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool ModifyUser(User user);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool RemoveUser(int id);
        /// <summary>
        /// 查询全部用户
        /// </summary>
        /// <returns></returns>
        List<User> GetUsers();
        /// <summary>
        /// 按ID查用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUser(int id);
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        User ValidateUser(string userName, string password);

        /// <summary>
        /// 按照用户ID获取用户权限
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        List<Permission> GetPermissionByUserID(int id);


    }
}
