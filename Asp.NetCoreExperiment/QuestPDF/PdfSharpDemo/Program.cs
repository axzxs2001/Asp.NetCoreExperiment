using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Snippets.Font;
using System;

namespace PdfSharpExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            PDFWriter();
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            Console.WriteLine($"PDF文件已保存到");
            Console.ReadLine();
        }
        static void PDFWriter()
        {
            GlobalFontSettings.FontResolver = new CustomFontResolver();

            //var font = new XFont("TakaoGothic.ttf", 10);
            var font = new XFont("TakaoGothic", 10);
            var pen = new XPen(XColors.Black, 0.5);
            using var document = new PdfDocument();
            using var filestream = new System.IO.FileStream("output.pdf", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
            var pageCount = 50;
            for (var c = 0; c < pageCount; c++)
            {
                var page = document.AddPage(new PdfPage { Width = new XUnit(1100) });

                var gfx = XGraphics.FromPdfPage(page);
                // 设定表格的起始位置和大小
                var startX = 50;
                var startY = 50;
                var rowHeight = 20;
                var columnWidth = 100;
                var rows = 35;
                var columns = 10;

                // 绘制表格
                for (int i = 0; i <= rows; i++)
                {
                    gfx.DrawLine(pen, startX, startY + (i * rowHeight), startX + (columnWidth * columns), startY + (i * rowHeight));
                }

                for (int j = 0; j <= columns; j++)
                {
                    gfx.DrawLine(pen, startX + (j * columnWidth), startY, startX + (j * columnWidth), startY + (rowHeight * rows));
                }

                // 添加文本到表格
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        gfx.DrawString($"お金の流れ{c} {i + 1}-{j + 1}", font, XBrushes.Black, new XRect(startX + j * columnWidth, startY + i * rowHeight, columnWidth, rowHeight), XStringFormats.Center);
                    }
                }
                gfx.DrawString($"{c + 1}/{pageCount}页", font, XBrushes.Black, new XRect(startX + columns, startY + rows * rowHeight, columnWidth, rowHeight), XStringFormats.Center);

                document.Save(filestream);
            }
            document.Close();
            document.Dispose();
        }
    }



public class CustomFontResolver : IFontResolver
    {
        public string DefaultFontName => "TakaoGothic";

        public byte[] GetFont(string faceName)
        {
            if (faceName == "TakaoGothic")
            {
                // 替换为你的字体文件路径
                return File.ReadAllBytes(@"TakaoGothic.ttf");
            }

            // 返回默认字体，如果没有找到指定字体
            return null;
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("TakaoGothic", StringComparison.InvariantCultureIgnoreCase))
            {
                return new FontResolverInfo("TakaoGothic");
            }

            // 返回默认字体
            return new FontResolverInfo(DefaultFontName);
        }
    }

}
