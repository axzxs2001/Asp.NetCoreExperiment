
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
namespace DIChainOfResponsibility
{
    /// <summary>
    /// email支付明细通知
    /// </summary>
    public class StarPayMailTransfer : StarPayTransfer
    {
        /// <summary>
        /// 职责链通知方法
        /// </summary>
        /// <param name="detailEntity">通知内容</param>
        /// <returns></returns>
        public override bool Transfer(TransferParmeter transferParmeter)
        {
            var result = SendEmail(transferParmeter);
            return  _starPayTransfer.Transfer(transferParmeter)&& result;
        }
        bool SendEmail(TransferParmeter transferParmeter)
        {
            Console.WriteLine("-------------------------------------------EMAIL");
            return true;
        }
    }
}
