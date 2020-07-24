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
            var jis = Encoding.GetEncoding("shift_jis");       
            File.WriteAllText(Directory.GetCurrentDirectory() + "/a.txt", "こんにちは", jis);  
        }
    }
}
