using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulStandard01.Model
{
    /// <summary>
    /// 用户分页实体
    /// </summary>
    public class UserPagination : PaginationBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UserType
        { get; set; }
  
    }


}
