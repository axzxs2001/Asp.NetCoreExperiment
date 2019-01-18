using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UFT8_Bom_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Bom2();
            Console.WriteLine("Hello World!");
        }
        static void Bom1()
        {
            //通过实例化UTF8Encoding加构造参数
            var sss = new UTF8Encoding(true);
            var reader = new StreamWriter(Directory.GetCurrentDirectory() + "/a.txt", false, sss);
            reader.WriteLine("aaa");
            reader.Close();
        }
        static void Bom2()
        {
            //通过实例化UTF8Encoding加构造参数
            var list = new List<byte>(Encoding.UTF8.GetBytes("sss"));
            list.InsertRange(0, new byte[] { 239, 187, 191 });
            var stream = new FileStream(Directory.GetCurrentDirectory() + "/a.txt",FileMode.CreateNew);
            stream.Write(list.ToArray(), 0, list.Count);
            stream.Close();
        }
    }
}
