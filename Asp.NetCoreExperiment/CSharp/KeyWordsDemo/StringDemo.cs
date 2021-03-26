using System;
using System.Text;
using static System.Console;

namespace KeyWordsDemo
{
    class StringDemo : Demo
    {
        public unsafe void Run()
        {
            //string a = "abcd";
            //string b = "abcd";
            //fixed (char* p = a)
            //{
            //    WriteLine("原a字符串地址= 0x{0:x}", (int)p);

            //}
            //fixed (char* p = b)
            //{
            //    WriteLine("原b字符串地址= 0x{0:x}", (int)p);

            //}
            //b = b + "e";
            //fixed (char* p = b)
            //{
            //    WriteLine("新b字符串地址= 0x{0:x}", (int)p);

            //}
            //WriteLine("------------------");
            //Console.WriteLine("IsNullOrWhiteSpace(null):" + string.IsNullOrWhiteSpace(null));
            //Console.WriteLine("IsNullOrWhiteSpace(\"\"):" + string.IsNullOrWhiteSpace(""));
            //Console.WriteLine("IsNullOrWhiteSpace(\" \"):" + string.IsNullOrWhiteSpace(" "));
            //Console.WriteLine("IsNullOrEmpty(null):" + string.IsNullOrEmpty(null));
            //Console.WriteLine("IsNullOrEmpty(\"\"):" + string.IsNullOrEmpty(""));
            //Console.WriteLine("IsNullOrEmpty(\" \"):" + string.IsNullOrEmpty(" "));
            WriteLine("---------StringBuilder---------");
            var contentBuilder = new StringBuilder();
            contentBuilder.AppendLine("line00001");
            contentBuilder.AppendLine("line00002");
            contentBuilder.AppendLine("line00003");
            var content = contentBuilder.ToString();
            Console.WriteLine(content);
         
            Console.WriteLine(content.Replace("\r", "\\r").Replace("\n", "\\n"));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("---------Environment.NewLine-------- ");
            content = "line0000A" + Environment.NewLine + "line0000B";
            Console.WriteLine(content);
            Console.WriteLine();
            Console.WriteLine(content.Replace("\r", "\\r").Replace("\n", "\\n"));
            ReadLine();
        }
    }

}
