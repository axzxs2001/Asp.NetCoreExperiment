using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace SharpZIPDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //直压文件夹
            // ZipFile.CreateFromDirectory(@"C:\myfile\190605", @"C:\myfile\190605.zip");

            //不支持密码

            //File.Delete(@"C:\myfile\190605.zip");
            //using (var compressedFileStream = new FileStream(@"C:\myfile\190605.zip", FileMode.CreateNew, FileAccess.Write))
            //{             

            //    using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
            //    {

            //        var files =  @"C:\myfile\190605";
            //        DG(zipArchive, files, "文件夹");                   
            //    }
            //}
         
            //第三种方法

            ZipStrings.CodePage = Encoding.UTF8.CodePage;
            var stream = new MemoryStream();
            //var zipFile = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(stream);
            var zipFile = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(@"C:\myfile\ziptest\abc.zip"); 
            zipFile.BeginUpdate();
            zipFile.Password = "111111"; 
            foreach (var file in Directory.GetFiles(@"C:\myfile\ziptest\190605"))
            {              
                zipFile.Add(file, "/aaa/" + Path.GetFileName(file));
            }          
            zipFile.CommitUpdate();
            zipFile.Close();


        }




        static void DG(ZipArchive zipArchive, string path, string parentPath)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                var zipEntry = zipArchive.CreateEntry($@"{parentPath}/{Path.GetFileName(file)}");

                using (var originalFileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    using (var zipEntryStream = zipEntry.Open())
                    {

                        originalFileStream.CopyTo(zipEntryStream);
                    }
                }
            }
            foreach (var dir in Directory.GetDirectories(path))
            {
                DG(zipArchive, dir, $"{parentPath}/{Path.GetFileName(dir)}");
            }
        }
    }
}
