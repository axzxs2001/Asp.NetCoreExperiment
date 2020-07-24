using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model
{

    /// <summary>
        /// 用户角色关系
        /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID
        { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }
    }
}
