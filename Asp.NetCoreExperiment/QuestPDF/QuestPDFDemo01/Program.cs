// See https://aka.ms/new-console-template for more information


using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data;
using System.Data.Common;

static MemoryStream FFF(DataTable dt)
{

    var stream = new MemoryStream();
    Document.Create(container =>
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


                            static IContainer CellStyle(IContainer container)
                            {
                                return container.DefaultTextStyle(x => x.SemiBold().FontSize(11)).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
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

                            static IContainer CellStyle(IContainer container)
                            {
                                return container.DefaultTextStyle(x => x.SemiBold().FontSize(11)).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
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
    }).GeneratePdf(stream);
    return stream;

}


static void F()
{



    File.Delete("hello.pdf");
    var products = new List<Product> {
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },

};

    Document.Create(container =>
    {
        container.Page(page =>
        {

            page.Size(PageSizes.A4);
            page.Margin(2, Unit.Centimetre);
            page.PageColor(Colors.White);

            page.DefaultTextStyle(x => x.FontSize(14).FontFamily("Meiryo"));

            page.Header()
                .Text("⾼速接続")
                .SemiBold().FontSize(24).FontColor(Colors.Blue.Medium);


            page.Content()
                .PaddingVertical(1, Unit.Centimetre)
                .Column(x =>
                {
                    x.Spacing(4);

                    x.Item().DefaultTextStyle(x => x.Underline()).Text("より大きな 13 インチのタ");
                    x.Item().DefaultTextStyle(x => x.FontSize(15)).AlignRight().Text(DateTime.Now.ToString("yyyy/MM/dd"));
                    x.Item().Text("ッチスクリーンや高速接続");
                    x.Item().Text("より多くをこなすことができま");
                    x.Item().Text("ッチスクリーンや高速接続");
                    x.Item().Text("り多くをこなすことができます");
                    x.Item().Text("こなすことができます。Windows 11 が搭載されています。");
                    x.Item().LineHorizontal(2f, Unit.Point);
                    // x.Item().Image(Placeholders.Image(200, 100));
                    x.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(25);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });


                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("SN");
                            header.Cell().Element(CellStyle).Text("Product");
                            header.Cell().Element(CellStyle).AlignRight().Text("Unit price");
                            header.Cell().Element(CellStyle).AlignRight().Text("Quantity");
                            header.Cell().Element(CellStyle).AlignRight().Text("Total");

                            static IContainer CellStyle(IContainer container)
                            {
                                return container.DefaultTextStyle(x => x.SemiBold().FontSize(11)).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                            }
                        });

                        var i = 0;
                        foreach (var product in products)
                        {
                            if (i % 2 == 0)
                            {
                                table.Cell().Element(CellStyle).Text(products.IndexOf(product) + 1);
                                table.Cell().Element(CellStyle).Text(product.Name);
                                table.Cell().Element(CellStyle).AlignRight().Text($"{product.Price}$");
                                table.Cell().Element(CellStyle).AlignRight().Text(product.Quantity);
                                table.Cell().Element(CellStyle).AlignRight().Text($"{product.Total}$");
                                static IContainer CellStyle(IContainer container)
                                {

                                    return container.DefaultTextStyle(x => x.FontSize(11)).Background(Colors.Grey.Lighten4).BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);

                                }
                            }
                            else
                            {
                                table.Cell().Element(CellStyle1).Text(products.IndexOf(product) + 1);
                                table.Cell().Element(CellStyle1).Text(product.Name);
                                table.Cell().Element(CellStyle1).AlignRight().Text($"{product.Price}$");
                                table.Cell().Element(CellStyle1).AlignRight().Text(product.Quantity);
                                table.Cell().Element(CellStyle1).AlignRight().Text($"{product.Total}$");
                                static IContainer CellStyle1(IContainer container)
                                {
                                    return container.DefaultTextStyle(x => x.FontSize(11)).BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                                }
                            }
                            i++;
                        }
                        x.Item().DefaultTextStyle(x => x.FontSize(9)).Text("(注) ッチスクリーンや高速接続、スピードアップで、より多くをこなすことができます。ッチスクリーンや高速接続、スピードアップで、より多くをこなすことができますッチスクリーンや高速接続、スピードアップで、より多くをこなすことができます");

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
    })
    .GeneratePdf("hello.pdf");

    Console.WriteLine("生成结束!");
}

class Product
{

    public string Name { get; set; }

    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get => Price * Quantity; }

}