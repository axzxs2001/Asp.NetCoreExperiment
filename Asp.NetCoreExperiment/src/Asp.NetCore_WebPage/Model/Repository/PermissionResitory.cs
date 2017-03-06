using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model.Repository
{
    /// <summary>
    /// 权限仓储类
    /// </summary>
    public class PermissionResitory : IPermissionResitory
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        ExperimentPageContext _dbContext;
        /// <summary>
        /// 权限仓储类构造
        /// </summary>
        /// <param name="dbContext">startup注入的数据库对象</param>
        public PermissionResitory(ExperimentPageContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool AddUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                var result = _dbContext.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }
        /// <summary>
        /// 按照ID查询对象
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public User GetUser(int id)
        {
            try
            {
                return _dbContext.Users.SingleOrDefault(w => w.ID == id);
            }
            catch (Exception exc)
            {
                return null;
            }
        }
        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            try
            {
                return _dbContext.Users.ToList();
            }
            catch (Exception exc)
            {
                return new List<User>();
            }
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ModifyUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                var result = _dbContext.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }
        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveUser(int id)
        {
            try
            {
                var user = _dbContext.Users.SingleOrDefault(w => w.ID == id);
                _dbContext.Users.Remove(user);
                var result = _dbContext.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public User ValidateUser(string userName, string password)
        {
            return _dbContext.Users.SingleOrDefault(s => s.UserName == userName && s.Password == password);
        }

        /// <summary>
        /// 按照用户ID获取用户权限
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public List<Permission> GetPermissionByUserID(int id)
        {
            var permissonDic = new Dictionary<int, Permission>();
            var roleIds = _dbContext.UserRoles.Where(u => u.UserID == id).Select(s => new { s.RoleID }).ToList();
            foreach (var roleid in roleIds)
            {
                var roleId = Convert.ToInt32(roleid.RoleID);
                foreach (var rolePermission in _dbContext.RolePermissions.Where(w => w.RoleID == roleId).ToList())
                {
                    if (!permissonDic.Keys.Contains(rolePermission.PermissionID))
                    {
                        var permission = _dbContext.Permissions.SingleOrDefault(s => s.ID == rolePermission.PermissionID);
                        permissonDic.Add(rolePermission.PermissionID, permission);
                    }
                }
            }
            return permissonDic.Values.ToList();
        }
    }
}
