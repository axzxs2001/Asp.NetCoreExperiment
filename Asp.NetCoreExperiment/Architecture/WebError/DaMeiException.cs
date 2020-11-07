using System;

namespace WebError
{
    /// <summary>
    /// 产品异常
    /// </summary>
    public class DaMeiException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public DaMeiException(string message) : base(message)
        {

        }
    }
    /// <summary>
    /// His项目异常
    /// </summary>
    public class HisException : DaMeiException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public HisException(string message) : base(message)
        {

        }
    }
    /// <summary>
    /// Lis项目异常
    /// </summary>
    public class LisException : DaMeiException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public LisException(string message) : base(message)
        {

        }
    }
    /// <summary>
    /// 模块异常
    /// </summary>
    public class RegisteredException : HisException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public RegisteredException(string message) : base(message)
        {

        }
    }
}
