// See https://aka.ms/new-console-template for more information


using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


Console.WriteLine("Hello, World!");

File.Delete("hello.pdf");
var products = new List<Product> {
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
    new Product{ Name="a产品", Price=12.5m, Quantity=10 },
    new Product{ Name="b产品", Price=2.5m, Quantity=10 },
    new Product{ Name="c产品", Price=1.5m, Quantity=10 },
};

Document.Create(container =>
{
    container.Page(page =>
    {

        page.Size(PageSizes.A4);
        page.Margin(2, Unit.Centimetre);
        page.PageColor(Colors.White);
        page.DefaultTextStyle(x => x.FontSize(20).FontFamily("FangSong"));

        page.Header()

            .Text("这是标题")
            .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

        page.Content()
            .PaddingVertical(1, Unit.Centimetre)
            .Column(x =>
            {
                x.Spacing(20);

                x.Item().Text("より大きな 13 インチのタッチスクリーンや高速接続、スピードアップで、より多くをこなすことができます。Windows 11 が搭載されています。");
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

                    // step 2
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("#");
                        header.Cell().Element(CellStyle).Text("Product");
                        header.Cell().Element(CellStyle).AlignRight().Text("Unit price");
                        header.Cell().Element(CellStyle).AlignRight().Text("Quantity");
                        header.Cell().Element(CellStyle).AlignRight().Text("Total");

                        static IContainer CellStyle(IContainer container)
                        {
                            return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                        }
                    });

                    // step 3
                    foreach (var product in products)
                    {
                        table.Cell().Element(CellStyle).Text(products.IndexOf(product) + 1);
                        table.Cell().Element(CellStyle).Text(product.Name);
                        table.Cell().Element(CellStyle).AlignRight().Text($"{product.Price}$");
                        table.Cell().Element(CellStyle).AlignRight().Text(product.Quantity);
                        table.Cell().Element(CellStyle).AlignRight().Text($"{product.Total}$");

                        static IContainer CellStyle(IContainer container)
                        {
                            return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
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
            });
    });
})
.GeneratePdf("hello.pdf");

class Product
{

    public string Name { get; set; }

    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get => Price * Quantity; }

}