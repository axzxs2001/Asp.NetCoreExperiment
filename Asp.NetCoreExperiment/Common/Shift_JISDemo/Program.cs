using System;
using System.Text;
using System.IO;
namespace Shift_JISDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var ens = Encoding.GetEncodings();
            var jis = System.Text.Encoding.GetEncoding("shift_jis");
            var bytes = jis.GetBytes("こんにちは");
            File.WriteAllText(Directory.GetCurrentDirectory() + "/a.txt", "こんにちは", jis);
            Console.WriteLine("Hello World!");
        }
    }
}
