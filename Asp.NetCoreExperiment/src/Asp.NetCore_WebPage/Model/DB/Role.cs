using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model
{
    /// <summary>
        /// 角色表
        /// </summary>
    public class Role
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID
        { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName
        {
            get; set;
        }
        /// <summary>
        /// 用户角色列表
        /// </summary>
        public List<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 角色权限列表
        /// </summary>
        public List<RolePermission> RolePermissions { get; set; }
    }
}
