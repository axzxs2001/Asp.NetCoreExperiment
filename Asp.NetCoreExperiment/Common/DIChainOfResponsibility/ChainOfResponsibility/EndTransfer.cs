using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace DIChainOfResponsibility
{
    /// <summary>
    /// 最后的通知
    /// </summary>
    public class EndTransfer : ParentTransfer
    {
        readonly ILogger<FirstTransfer> _logger;
        public EndTransfer(ILogger<FirstTransfer> logger)
        {
            _logger = logger;        
        }
        /// <summary>
        /// 职责链通知方法
        /// </summary>
        /// <param name="detailEntity">通知内容</param>
        /// <returns></returns>
        public override bool Transfer(TransferParmeter noticeParmeter)
        {
            _logger.LogInformation("-------------------------------------------EndTransfer");
            return true;
        }
    }
}
