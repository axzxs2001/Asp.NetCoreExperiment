using System;
using System.Text;
using System.Text.RegularExpressions;
namespace HalfFullWidthDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("UTF8:"+System.Text.Encoding.UTF8.GetByteCount("ＡA你"));
            Console.WriteLine("UTF7:" + System.Text.Encoding.UTF7.GetByteCount("ＡA你"));
            Console.WriteLine("UTF32:" + System.Text.Encoding.UTF32.GetByteCount("ＡA你"));
            Console.WriteLine("Default:" + System.Text.Encoding.Default.GetByteCount("ＡA你"));
            Console.WriteLine("Unicode:" + System.Text.Encoding.Unicode.GetByteCount("ＡA你"));
            Console.WriteLine("ASCII:" + System.Text.Encoding.ASCII.GetByteCount("ＡA你"));

            var end = Encoding.GetEncoding(50220); 
            Console.WriteLine("shift-jis:" + end.GetByteCount("ＡA你"));

            var regex = new Regex(@"^\s*\S((.){0,4}\S)?\s*$");
            Console.WriteLine(regex.IsMatch("ＡAAA"));
        }
    }
}
