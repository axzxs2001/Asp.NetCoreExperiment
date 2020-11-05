using System;

namespace ArchitectureDemo06
{
    class Program
    {
        static void Main()
        {
            try
            {            
                throw new HisException("his数据库未初始化！");
            }
            catch (RegisteredException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (HisException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (DaMeiException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            finally
            {

            }
            Console.WriteLine("");
        }
    }

    /// <summary>
    /// 大美医疗信息系统，这是我起的名，整个系统异常类
    /// </summary>
    public class DaMeiException : ApplicationException
    {
        public DaMeiException(string message) : base(message)
        {
            Console.WriteLine("{0},{1}", DateTime.UtcNow.ToString("yyyy-MM-ss HH:mm:ss"), message);
        }
        public DaMeiException() : base()
        {
        }
    }
    /// <summary>
    /// His 异常类
    /// </summary>
    public class HisException : DaMeiException
    {
        public HisException(string message) : base(message)
        {
        }
        public HisException() : base()
        {
        }
    }
    /// <summary>
    /// 挂号模块异常类
    /// </summary>
    public class RegisteredException : HisException
    {
        public RegisteredException(string message) : base(message)
        {
        }
        public RegisteredException() : base()
        {
        }
    }
    /// <summary>
    /// Lis 异常类
    /// </summary>
    public class LisException : DaMeiException
    {
        public LisException(string message) : base(message)
        {
        }
        public LisException() : base()
        {
        }

    }
}
