using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model
{



    /// <summary>
    /// 角色权限关系
    /// </summary>
    public class RolePermission
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public int PermissionID
        { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID
        { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public Permission Permission { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }
    }
}
