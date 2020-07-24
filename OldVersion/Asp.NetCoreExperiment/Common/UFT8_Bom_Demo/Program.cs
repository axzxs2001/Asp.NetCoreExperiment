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
            GenerateBom1();
            GenerateBom2();      
        }
        /// <summary>
        /// 方式一
        /// </summary>
        static void GenerateBom1()
        {
            var encoding = new UTF8Encoding(true);
            var filePath = Directory.GetCurrentDirectory() + "/bom1.txt";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (var reader = new StreamWriter(filePath, false, encoding))
            {
                reader.WriteLine("bom1");
                reader.Close();
            }
        }
        /// <summary>
        /// 方式二
        /// </summary>
        static void GenerateBom2()
        {
            var list = new List<byte>(Encoding.UTF8.GetBytes("bom2"));
            list.InsertRange(0, new byte[] { 239, 187, 191 });// EF=239 BB=187 BF=191
            var filePath = Directory.GetCurrentDirectory() + "/bom2.txt";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (var stream = new FileStream(filePath, FileMode.CreateNew))
            {
                stream.Write(list.ToArray(), 0, list.Count);
                stream.Close();
            }
        }
    }
}
