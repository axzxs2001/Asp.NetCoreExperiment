using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ExcelDemoClassLibrary01;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ExcelDemo01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //// GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    //加载Excel文件
        //    string filePath = $"{Directory.GetCurrentDirectory()}/test.xlsx";
        //    var file = new FileInfo(filePath);
        //    using (var package = new ExcelPackage(file))
        //    {
        //        var sheet = package.Workbook.Worksheets[1];
        //    }

        //    return new string[] { "value1", "value2" };
        //}

        #region
        private static ExcelPackage CreateExcelPackage<T>(List<T> datas, Dictionary<string, string> columnNames, List<string> outOfColumns, string sheetName = "Sheet1", string title = "", int isProtected = 0)
        {
            var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add(sheetName);
            worksheet.View.ShowGridLines = false;
            if (isProtected == 1)
            {
                worksheet.Protection.IsProtected = true;//设置是否进行锁定
                worksheet.Protection.SetPassword("xiangzhidaomimama");//设置密码
                worksheet.Protection.AllowAutoFilter = false;//下面是一些锁定时权限的设置
                worksheet.Protection.AllowDeleteColumns = false;
                worksheet.Protection.AllowDeleteRows = false;
                worksheet.Protection.AllowEditScenarios = false;
                worksheet.Protection.AllowEditObject = false;
                worksheet.Protection.AllowFormatCells = false;
                worksheet.Protection.AllowFormatColumns = false;
                worksheet.Protection.AllowFormatRows = false;
                worksheet.Protection.AllowInsertColumns = false;
                worksheet.Protection.AllowInsertHyperlinks = false;
                worksheet.Protection.AllowInsertRows = false;
                worksheet.Protection.AllowPivotTables = false;
                worksheet.Protection.AllowSelectLockedCells = false;
                worksheet.Protection.AllowSelectUnlockedCells = false;
                worksheet.Protection.AllowSort = false;
            }

            var titleRow = 0;
            if (!string.IsNullOrWhiteSpace(title))
            {
                titleRow = 1;
                worksheet.Cells[1, 1, 3, columnNames.Count()].Merge = true;//合并单元格
                worksheet.Cells[1, 1].Value = title;
                worksheet.Cells.Style.WrapText = true;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
                worksheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//垂直居中
                //worksheet.Row(1).Height = 30;//设置行高
                worksheet.Cells.Style.ShrinkToFit = true;//单元格自动适应大小
                //合并后的单元格加边框。
                worksheet.Cells[1, 1, 3, columnNames.Count].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            }
            titleRow = 5;
            //获取要反射的属性,加载首行
            var myType = typeof(T);
            var myPro = new List<PropertyInfo>();
            int i = 1;
            foreach (string key in columnNames.Keys)
            {
                var p = myType.GetProperty(key);
                myPro.Add(p);

                worksheet.Cells[1 + titleRow, i].Value = columnNames[key];
                i++;
            }

            int row = 2 + titleRow;
            foreach (T data in datas)
            {
                int column = 1;
                foreach (var p in myPro.Where(info => !outOfColumns.Contains(info.Name)))
                {
                    worksheet.Cells[row, column].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);//设置单元格所有边框
                    //worksheet.Cells[row, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //worksheet.Cells[row, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //worksheet.Cells[row, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;ww
                    //worksheet.Cells[row, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[row, column].Value = p == null ? "" : Convert.ToString(p.GetValue(data, null));
                    column++;
                }
                row++;
            }

            //worksheet.Cells[1, 10].Style.WrapText = true;
            worksheet.Cells[1, 10].Value = "123456789123456789";
            worksheet.Column(10).Width = 100;
            worksheet.Cells[5, 5, 10, 10].Merge = true;
            worksheet.Cells[5, 5, 10, 10].Value = "2222";
            worksheet.Cells[5, 5, 10, 10].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            worksheet.Cells[5, 5, 10, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[5, 5, 10, 10].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 199, 206));
            return package;
        }

        public static Byte[] GetByteToExportExcel()
        {
            //using (var fs = new MemoryStream())
            //{
            //    var t = DateTime.Now;
            //    //using (var package = CreateExcelPackage(datas, columnNames, outOfColumn, sheetName, title, isProtected))
            //    var excel = new DomeEPPlus();
            //    for (int i = 0; i < 10; i++)
            //    {
            //        excel.Data.AddRange(new Item[] {
            //            new Item { Amount = 1000, Code = "0001", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" },
            //            new Item { Amount = 1000, Code = "0002", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" },
            //            new Item { Amount = 1000, Code = "0003", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" },
            //            new Item { Amount = 1000, Code = "0004", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" } });
            //    }

            //    using (var package = excel.GetExcelPackage())
            //    {
            //        package.SaveAs(fs);
            //        Console.WriteLine("时间：" + (DateTime.Now - t).TotalSeconds);
            //        return fs.ToArray();
            //    }
            //}


        //XSSFWorkbook
        //var t = DateTime.Now;
        //var excel = new DemoXSSFWorkbook();
        //for (int i = 0; i < 20; i++)
        //{
        //    excel.Data.AddRange(new Item[] {
        //            new Item { Amount = 1000, Code = "0001", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" },
        //            new Item { Amount = 1000, Code = "0002", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" },
        //            new Item { Amount = 1000, Code = "0003", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" },
        //            new Item { Amount = 1000, Code = "0004", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" } });
        //}
        //var arr = excel.GetExcelPackage().ToArray();
        //Console.WriteLine("时间：" + (DateTime.Now - t).TotalSeconds);
        //return arr;

        //SXSSFWorkbook
        //var t = DateTime.Now;
        //var excel = new DemoXSSFWorkbook();
        //for (int i = 0; i < 20; i++)
        //{
        //    excel.Data.AddRange(new Item[] {
        //            new Item { Amount = 1000, Code = "0001", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" },
        //            new Item { Amount = 1000, Code = "0002", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" },
        //            new Item { Amount = 1000, Code = "0003", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" },
        //            new Item { Amount = 1000, Code = "0004", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" } });
        //}
        //var arr = excel.GetExcelPackage().ToArray();
        //Console.WriteLine("时间：" + (DateTime.Now - t).TotalSeconds);
        //return arr;


        //HSSFWorkbook
        var t = DateTime.Now;
        var excel = new DemoHSSFWorkbook();
            for (int i = 0; i< 1; i++)
            {
                excel.Data.AddRange(new Item[] {
                        new Item { Amount = 1000, Code = "0001", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" },
                        new Item { Amount = 1000, Code = "0002", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" },
                        new Item { Amount = 1000, Code = "0003", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" },
                        new Item { Amount = 1000, Code = "0004", Memo = "无", Name = "一号", Rate = 0.02d, RateAmount = 1000, TimeSection = "1月" } });
            }
            var arr = excel.GetExcelPackage().ToArray();
Console.WriteLine("时间：" + (DateTime.Now - t).TotalSeconds);
            return arr;
        }

        public async Task<IActionResult> GetExcel(int isProtected = 0)
        {      
            var fs = GetByteToExportExcel();
            //return File(fs, "application/vnd.android.package-archive", $"{DateTime.Now.ToString("ddHHmmssfff")}.xlsx");
            return File(fs, "application/vnd.android.package-archive", $"{DateTime.Now.ToString("ddHHmmssfff")}.xls");
        }

        #endregion
    }
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Remark { get; set; }
    }
}