using System.Data;
using System.Reflection.Metadata;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QRCoder;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Drawing;
using QuestPDF;

using var stream = File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fonts", "MEIRYO.TTC"));
FontManager.RegisterFont(stream);

Settings.EnableCaching = true;
Settings.EnableDebugging = false;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/test", () =>
{
    return "ok" + DateTime.Now;
});

app.MapGet("/getpdf", () =>
{
    var table = new DataTable();
    for (var i = 0; i < 10; i++)
    {
        table.Columns.Add(i.ToString());
    }
    for (var row = 0; row < 1000; row++)
    {
        table.Rows.Add(row.ToString(), row.ToString(), row.ToString(), row.ToString(), row.ToString(), row.ToString(), row.ToString(), DateTime.Now + "wwewrwerewfdsfdswefwefewfwefwefew" + row, row.ToString(), row.ToString());
    }
    GC.Collect();
    return TypedResults.File(GetPDF(table), contentType: "application/pdf", fileDownloadName: "a.pdf");

});

app.Run();
static IContainer CellStyle(IContainer container)
{
    return container.DefaultTextStyle(x => x.SemiBold().FontSize(11)).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
}
static byte[] GetPDF(DataTable dt)
{
    var doc = QuestPDF.Fluent.Document.Create(container =>
    {

        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(2, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(14).FontFamily("Meiryo"));
            page.Content()
                .PaddingVertical(1, Unit.Centimetre)
                .Column(x =>
                {
                    x.Item().Table(table =>
                    {

                        table.ColumnsDefinition(columns =>
                        {
                            for (var i = 0; i < dt.Columns.Count; i++)
                            {
                                columns.RelativeColumn();

                            }

                        });

                        table.Header(header =>
                        {

                            foreach (DataColumn col in dt.Columns)
                            {
                                header.Cell().Element(CellStyle).Text(col.ColumnName);
                            }

                        });
                        foreach (DataRow row in dt.Rows)
                        {
                            for (var i = 0; i < dt.Columns.Count; i++)
                            {
                                if (i == 7)
                                {
                                    byte[] qrCodeAsBitmapByteArr = PngByteQRCodeHelper.GetQRCode(row[i].ToString(), QRCodeGenerator.ECCLevel.Q, 20, false);
                                    using var ms = new MemoryStream(qrCodeAsBitmapByteArr);
                                    table.Cell().Image(qrCodeAsBitmapByteArr);
                                }
                                else
                                {
                                    table.Cell().Element(CellStyle).Text(row[i].ToString());
                                }
                            }
                        }

                    });
                });
            page.Footer()
                .AlignCenter()
                .Text(x =>
                {
                    x.Span("Page ");
                    x.CurrentPageNumber();
                    x.Span("/ ");
                    x.TotalPages();

                });
        });
    });
    return doc.GeneratePdf();
}
