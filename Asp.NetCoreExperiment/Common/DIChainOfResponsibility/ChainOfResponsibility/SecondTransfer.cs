
using Microsoft.Extensions.Logging;
using System;

namespace DIChainOfResponsibility
{

    public class SecondTransfer : ParentTransfer
    {
        readonly ILogger<SecondTransfer> _logger;
        public SecondTransfer(ILogger<SecondTransfer> logger, ThirdTransfer serviceAccessor)
        {
            _logger = logger;
            this.Next(serviceAccessor);
        }
        /// <summary>
        /// 职责链通知方法
        /// </summary>
        /// <param name="transferParmeter">通知内容</param>
        /// <returns></returns>
        public override bool Transfer(TransferParmeter transferParmeter)
        {
            var result = SelfTransfer(transferParmeter);
            return _parentTransfer.Transfer(transferParmeter) && result;
        }

        bool SelfTransfer(TransferParmeter transferParmeter)
        {
            _logger.LogInformation("-------------------------------------------SecondTransfer");
            return true;

        }


    }
}
