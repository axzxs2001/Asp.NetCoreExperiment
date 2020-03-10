using System;
using System.IO;
using System.Text;

namespace FileWriteAllTextDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = int.Parse(Console.ReadLine());
            var con = "";
            for (int i = 0; i < c; i++)
            {
                con += $"{i}\r\n";
            }
            using (var stream = File.OpenWrite("c:/myfile/a.txt"))
            {
                var arr = Encoding.UTF8.GetBytes(con);
                stream.Write(arr, 0, arr.Length);
            }
       
        }
    }
}
