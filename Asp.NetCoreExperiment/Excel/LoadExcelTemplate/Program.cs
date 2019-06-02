using System;
using System.IO;
using System.Reflection;
using OfficeOpenXml;
namespace LoadExcelTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = Directory.GetCurrentDirectory() + "\\Demo.xlsx";
            using (var source = System.IO.File.OpenRead(file))
            {
                using (var excel = new ExcelPackage(source))
                {

                    excel.Workbook.Worksheets[1].Cells[3, 1].Value = 123;
                    excel.Workbook.Worksheets[1].Cells[3, 2].Value = "张三丰";
                    excel.Workbook.Worksheets[1].Cells[3, 3].Value = 29;
                    excel.Workbook.Worksheets[1].Cells[3, 4].Value = true;
                    var newfile = Directory.GetCurrentDirectory() + "\\new.xlsx";
                    var newfileinfo = new FileInfo(newfile);
                    excel.SaveAs(newfileinfo);
                }
            }
        }
    }
}
