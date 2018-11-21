
namespace DIChainOfResponsibility
{
    /// <summary>
    /// 职责链通知抽象类
    /// </summary>
    public abstract class StarPayTransfer
    {
        /// <summary>
        /// StarPayTransfer
        /// </summary>
        protected StarPayTransfer _starPayTransfer;
        /// <summary>
        /// 传送下一个方法
        /// </summary>
        /// <param name="starPayTransfer"></param>
        public void Next(StarPayTransfer starPayTransfer)
        {
            _starPayTransfer = starPayTransfer;
        }
        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="detailEntity"></param>
        /// <returns></returns>
        public abstract bool Transfer(TransferParmeter transferParmeter);
    }
}
