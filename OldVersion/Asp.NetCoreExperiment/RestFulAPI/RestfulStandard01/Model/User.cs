using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulStandard01.Model
{
    /// <summary>
    /// web api验证是加在Modle上的
    /// </summary>
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MinLength(6, ErrorMessage = "长度不能小于6")]
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不能为空")]
        [UserNameEqualPassword("用户名和密码长度相同了")]
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不能为空")]
  
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UserType { get; set; }
    }
}
