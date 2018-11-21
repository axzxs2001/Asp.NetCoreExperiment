

using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace DIChainOfResponsibility
{
    /// <summary>
    /// ftp支付明细通知
    /// </summary>
    public class StarPayFtpTransfer : StarPayTransfer
    {
        /// <summary>
        /// 职责链通知方法
        /// </summary>
        /// <param name="detailEntity">通知内容</param>
        /// <returns></returns>
        public override bool Transfer(TransferParmeter transferParmeter)
        {
            var result = FTPTransfer(transferParmeter);
            return _starPayTransfer.Transfer(transferParmeter) && result;
        }
        bool FTPTransfer(TransferParmeter transferParmeter)
        {
            Console.WriteLine("-------------------------------------------FTP");
            return true;
        }

    }
}
