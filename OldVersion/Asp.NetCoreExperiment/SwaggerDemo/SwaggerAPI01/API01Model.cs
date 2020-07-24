using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAPI01
{
    /// <summary>
    /// 测试Model
    /// </summary>
    public class API01Model
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Describe
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// IsSure
        /// </summary>
        public bool IsSure { get; set; }
    }
}
