using DinkToPdf;
using System;
using System.IO;

namespace PDFDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4Plus,
                },
                Objects = {
                    new ObjectSettings()
                    {
                        Page = "http://google.com/",
                    },
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. In consectetur mauris eget ultrices  iaculis. Ut                               odio viverra, molestie lectus nec, venenatis turpis.",
                        WebSettings = {
                            DefaultEncoding = "utf-8"
                        },
                        HeaderSettings = {
                            FontSize = 9,
                            Right = "Page [page] of [toPage]",
                            Line = true, Spacing = 2.812
                        }
                    }
                }
            };
            var converter = new BasicConverter(new PdfTools());
            byte[] pdf = converter.Convert(doc);
            var file = new FileStream(Directory.GetCurrentDirectory() + $"/{DateTime.Now.ToString("MMddHHmmss")}.pdf", FileMode.CreateNew, FileAccess.ReadWrite);
            file.Write(pdf, 0, pdf.Length);
            file.Close();
        }
    }
}
