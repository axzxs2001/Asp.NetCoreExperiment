using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID
        {
            get; set;
        }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName
        {
            get; set;
        }
        /// <summary>
        /// action
        /// </summary>
        public string ActionName
        {
            get; set;
        }
        /// <summary>
        /// 角色权限列表
        /// </summary>
        public List<RolePermission> RolePermissions { get; set; }
    }
}
