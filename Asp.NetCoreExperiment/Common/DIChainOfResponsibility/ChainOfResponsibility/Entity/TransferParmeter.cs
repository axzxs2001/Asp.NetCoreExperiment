
using Microsoft.Extensions.Logging;

namespace DIChainOfResponsibility
{
    /// <summary>
    /// 通知参数
    /// </summary>
    public class TransferParmeter
    {      
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 转换ID
        /// </summary>
        public int TransferID { get; set; }
        /// <summary>
        /// 发送内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
    }
}
