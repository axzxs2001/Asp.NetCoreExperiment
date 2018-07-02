using Ocelot.JwtAuthorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAuthorize
{
    /// <summary>
    /// 
    /// </summary>
    public class BackResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public bool Result
        {
            get; set;
        }
        /// <summary>
        /// token数据
        /// </summary>
        public TokenBuilder.Token Data
        {
            get; set;
        }
    }
}
