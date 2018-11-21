using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;


namespace DIChainOfResponsibility
{
    /// <summary>
    /// 最后的通知
    /// </summary>
    public class EndTransfer : StarPayTransfer
    {
        /// <summary>
        /// 职责链通知方法
        /// </summary>
        /// <param name="detailEntity">通知内容</param>
        /// <returns></returns>
        public override bool Transfer(TransferParmeter noticeParmeter)
        {
            return true;
        }
    }
}
