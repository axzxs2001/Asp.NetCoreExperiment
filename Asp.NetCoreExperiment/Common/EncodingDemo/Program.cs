using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "ズフーェ0ｱｲｳｴｵ";
            Console.WriteLine("UTF8:" + System.Text.Encoding.UTF8.GetByteCount(s));
            Console.WriteLine("UTF7:" + System.Text.Encoding.UTF7.GetByteCount(s));
            Console.WriteLine("UTF32:" + System.Text.Encoding.UTF32.GetByteCount(s));
            Console.WriteLine("Default:" + System.Text.Encoding.Default.GetByteCount(s));
            Console.WriteLine("Unicode:" + System.Text.Encoding.Unicode.GetByteCount(s));
            Console.WriteLine("ASCII:" + System.Text.Encoding.ASCII.GetByteCount(s));
            var encoding = Encoding.GetEncoding(932);
            Console.WriteLine("shift-jis:" + encoding.GetByteCount(s));

        }
    }
}
