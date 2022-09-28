

using Aspose.Cells;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using QRCoder;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using Color = System.Drawing.Color;
using Workbook = Aspose.Cells.Workbook;

Console.WriteLine("回车开始");
Console.ReadLine();
var qrGenerator = new QRCodeGenerator();
while (true)
{
    var workBook = new XLWorkbook();
    var ws = workBook.Worksheets.Add("test");
    for (var i = 1; i < 200; i++)
    {

 

        //var qrCodeData = qrGenerator.CreateQrCode(DateTime.Now.ToString("yyMMddHHmmssfffffff") + "QRCodeGeneratoQRCodeGeneratoQRCodeGeneratoQRCodeGeneratoQRCodeGenerato", QRCodeGenerator.ECCLevel.L);
        //var qrCode = new QRCode(qrCodeData);
        //var qrCodeImage = qrCode.GetGraphic(10, Color.Black, Color.White, false);

        byte[] qrCodeAsBitmapByteArr = PngByteQRCodeHelper.GetQRCode(DateTime.Now.ToString("yyMMddHHmmssfffffff") + "QRCodeGeneratoQRCodeGeneratoodeGeneratoQRCodeGeneratoodeGeneratoQRCodeGeneratoodeGeneratoQRCodeGeneratoodeGeneratoQRCodeGeneratoodeGeneratoQRCodeGeneratoodeGeneratoQRCodeGeneratoodeGeneratoQRCodeGeneratoodeGeneratoQRCodeGeneratoodeGeneratoQRCodeGeneratoodeGeneratoQRCodeGeneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGenereneratoodeGeneratoQRCodeGeneratoodeGeneratoQRCodeGeneratoQRCodeGeneratoQRCodeGeneratoQRCodeGenerato", QRCodeGenerator.ECCLevel.Q, 20, false);
   


        var imgStream = new MemoryStream(qrCodeAsBitmapByteArr);
        //qrCodeImage.Save(imgStream, ImageFormat.Bmp);
        imgStream.Seek(0, SeekOrigin.Begin);


        var image = ws.AddPicture(imgStream).MoveTo(ws.Cell(i, 1), new Point(50, 10));
        image.Width = 60;
        image.Height = 60;
    }


    var tmpStream = new MemoryStream();
    workBook.SaveAs(tmpStream);
    tmpStream.Position = 0;
    var wb = new Workbook(tmpStream);
    var pdfSaveOptions = new PdfSaveOptions
    {
        OptimizationType = Aspose.Cells.Rendering.PdfOptimizationType.MinimumSize,
        OnePagePerSheet = true

    };
    //var ms = new MemoryStream();
    //wb.Save(ms, pdfSaveOptions);
    //ms.Position = 0;

    wb.Save($"C:\\Users\\axzxs\\Pictures\\{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf");

    Console.WriteLine($"完了:{DateTime.Now}");
    Console.ReadLine();

}


Console.ReadLine();
