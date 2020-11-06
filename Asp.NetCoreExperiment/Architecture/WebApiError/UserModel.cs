using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiError
{
    /// <summary>
    /// 用户模型
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///名称
        /// </summary>
        [MinLength(2)]
        [MaxLength(5, ErrorMessage = "长度不能超过5")]
        public string Name { get; set; }
    }
}
