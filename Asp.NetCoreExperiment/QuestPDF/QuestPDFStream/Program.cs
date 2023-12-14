using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var document = new MyDocument();
document.GeneratePdf(Directory.GetCurrentDirectory()+"/output.pdf");

public class MyDocument : IDocument
{
    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
    }

    void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeColumn().Text("Header");
        });
    }

    void ComposeContent(IContainer container)
    {
        // 这里可以根据需要添加更多的内容，例如文本、图像、表格等
        for (int i = 0; i < 1000; i++)
        {
            container.Text($"Line {i + 1}");
        }
    }

    void ComposeFooter(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeColumn().Text(x =>
            {
                x.CurrentPageNumber();
                x.Span(" / ");
                x.TotalPages();
            });
        });
    }


}

