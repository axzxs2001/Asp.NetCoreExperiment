
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DIChainOfResponsibility
{
    /// <summary>
    /// sftp支付明细通知
    /// </summary>
    public class StarPaySFtpTransfer : StarPayTransfer
    {
        /// <summary>
        /// 职责链通知方法
        /// </summary>
        /// <param name="detailEntity">通知内容</param>
        /// <returns></returns>
        public override bool Transfer(TransferParmeter transferParmeter)
        {
            var result = SFTPTransfer(transferParmeter);
            return _starPayTransfer.Transfer(transferParmeter) && result;
        }
        /// <summary>
        /// sftp发送
        /// </summary>
        /// <param name="transferParmeter">传输参数</param>
        bool SFTPTransfer(TransferParmeter transferParmeter)
        {
            Console.WriteLine("-------------------------------------------SFTP");
            return true;

        }


    }
}
