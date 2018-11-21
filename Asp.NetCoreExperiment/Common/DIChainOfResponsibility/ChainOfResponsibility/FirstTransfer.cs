
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
namespace DIChainOfResponsibility
{

    public class FirstTransfer : ParentTransfer
    {
        readonly ILogger<FirstTransfer> _logger;
        public FirstTransfer(ILogger<FirstTransfer> logger,SecondTransfer secondTransfer)
        {
            _logger = logger;
            this.Next(secondTransfer);
        }
        /// <summary>
        /// 职责链通知方法
        /// </summary>
        /// <param name="transferParmeter">通知内容</param>
        /// <returns></returns>
        public override bool Transfer(TransferParmeter transferParmeter)
        {
            var result = SelfTransfer(transferParmeter);
            return _parentTransfer.Transfer(transferParmeter)&& result;
        }
        bool SelfTransfer(TransferParmeter transferParmeter)
        {
            _logger.LogInformation("-------------------------------------------FirstTransfer");
            return true;
        }
    }
}
