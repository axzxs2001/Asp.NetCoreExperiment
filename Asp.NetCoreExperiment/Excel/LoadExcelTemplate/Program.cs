using System;
using System.Collections.Generic;
using System.Reflection;
using OfficeOpenXml;
using System.Linq;
using System.IO;
using Dapper;
using System.Data.SqlClient;

namespace LoadExcelTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var con = new SqlConnection("server=.;database=testdb;uid=sa;pwd=sa;"))
            {
                var sql = "select name,age,sex,height from works where id=1";
                IDictionary<string, object> work = con.Query<dynamic>(sql).FirstOrDefault();

                if (work != null)
                {
                    var file = Directory.GetCurrentDirectory() + "\\mywork.xlsx";
                    using (var source = System.IO.File.OpenRead(file))
                    {
                        using (var excel = new ExcelPackage(source))
                        {
                            var jsonFile = Directory.GetCurrentDirectory() + "\\excelmap.json";
                            var excelMaps = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ExcelMap>>(File.ReadAllText(jsonFile));
                            var fields = excelMaps.Select(s => s.Field);
                           
                            foreach (var excelMap in excelMaps)
                            {
                                
                                excel.Workbook.Worksheets[1].Cells[excelMap.Row, excelMap.Col].Value = work[excelMap.Field];
                            }
                            var newfile = Directory.GetCurrentDirectory() + "\\newmywork.xlsx";
                            var newfileinfo = new FileInfo(newfile);
                            excel.SaveAs(newfileinfo);
                        }
                    }
                }
            }
        }
        static void Demo01()
        {
            var file = Directory.GetCurrentDirectory() + "\\demo.xlsx";
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


    public class ExcelMap
    {
        public string Field { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
    }
}
