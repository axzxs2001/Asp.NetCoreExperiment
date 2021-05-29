using System;
using static System.Console;

namespace KeyWordsDemo
{
    class FilterExceptionDemo : IDemo
    {
        public void Run()
        {
            try
            {
                throw new Exception("异常");
            }
            catch (Exception exc) when (DateTime.Now > DateTime.Parse("2021-06-29"))
            {
                WriteLine($"条件：{exc.Message}");
            }
            catch (Exception exc)
            {
                WriteLine(exc.Message);
            }
        }
    }
}
