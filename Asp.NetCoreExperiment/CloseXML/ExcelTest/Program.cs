using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using QRCoder;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using Color = System.Drawing.Color;

Console.WriteLine("回车开始");
Console.ReadLine();
var qrGenerator = new QRCodeGenerator();
while (true)
{
    using var workBook = new XLWorkbook();
    var ws = workBook.Worksheets.Add("TestSheet");
    for (var i = 1; i < 200; i++)
    {
        //using var qrCodeData = qrGenerator.CreateQrCode($"{i}_{DateTime.Now.ToString("yyMMddHHmmssfffffff")}_01PAe8np5m7pVULUiuxwwZTWQ9KZ8JUgQyWiyUrsiHqi4FKrCzhRAcddCkkJKDgVEkpmqD7kYJz5GTpe4oHvJdJDnNMMCTwbV19G", QRCodeGenerator.ECCLevel.Q);
        //using var qrCode = new QRCode(qrCodeData);
        //using var qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, false);
        //using var imgStream = new MemoryStream();
        //qrCodeImage.Save(imgStream, ImageFormat.Png);
        byte[] qrCodeAsBitmapByteArr = PngByteQRCodeHelper.GetQRCode(DateTime.Now.ToString("yyMMddHHmmssfffffff") + "01PAe8np5m7pVULUiuxwwZTWQ9KZ8JUgQyWiyUrsiHqi4FKrCzhRAcddCkkJKDgVEkpmqD7kYJz5GTpe4oHvJdJDnNMMCTwbV19G", QRCodeGenerator.ECCLevel.Q, 20, false);
        using var imgStream = new MemoryStream(qrCodeAsBitmapByteArr);
        imgStream.Seek(0, SeekOrigin.Begin);

        using var image = ws.AddPicture(imgStream).MoveTo(ws.Cell(i, 1), new Point(50, 10));
        image.Width = 60;
        image.Height = 60;
    }
    workBook.SaveAs(@$"C:\Users\axzxs\Pictures\{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");
    Console.WriteLine($"完了:{DateTime.Now}");
    Console.ReadLine();
}

