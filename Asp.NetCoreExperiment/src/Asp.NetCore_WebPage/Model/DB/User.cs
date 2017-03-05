using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID
        { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        { get; set; }
        /// <summary>
        /// 用户角色集合
        /// </summary>
        public List<UserRole> UserRoles { get; set; }
    }
}
