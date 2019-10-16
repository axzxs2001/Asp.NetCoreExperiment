using System;
namespace DIChainOfResponsibility
{
    public interface ITransfer
    { 
        void Next(ITransfer parentTransfer);
        bool Transfer(TransferParmeter transferParmeter);
    }

    /// <summary>
    /// 职责链通知抽象类
    /// </summary>
    public abstract class ParentTransfer : ITransfer
    {
     

        protected ITransfer _parentTransfer;
        /// <summary>
        /// 传送下一个方法
        /// </summary>
        /// <param name="parentTransfer"></param>
        public void Next(ITransfer parentTransfer)
        {
            Console.WriteLine($"-------------{parentTransfer.GetType().Name}.Next()");
            _parentTransfer = parentTransfer;
        }
        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="detailEntity"></param>
        /// <returns></returns>
        public abstract bool Transfer(TransferParmeter transferParmeter);
    }
}
