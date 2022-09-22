

using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

Console.WriteLine("回车开始");
Console.ReadLine();

using var workBook = new XLWorkbook();
var ws = workBook.Worksheets.Add("test");
for (var i = 1; i < 200; i++)
{
    var image = ws.AddPicture("C:\\Users\\axzxs\\Pictures\\1.jpg").MoveTo(ws.Cell(i, 1), new Point(50, 10));
    image.Width = 60;
    image.Height = 60;
}

workBook.SaveAs($"C:\\Users\\axzxs\\Pictures\\{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");

Console.WriteLine("完了");
Console.ReadLine();
foreach (var p in ws.Pictures)
{
    p.Dispose();
}
foreach (var r in ws.Rows())
{
    r.Delete();
}
workBook.Dispose();
GC.Collect();

var t = GC.GetTotalAllocatedBytes();
var t1 = GC.GetAllocatedBytesForCurrentThread();



Console.ReadLine();
