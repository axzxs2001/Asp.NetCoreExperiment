using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RestfulStandard01.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccountType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccountNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UserID { get; set; }
    }
}
