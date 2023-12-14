using Aspose.Pdf;

using var fileStream = new FileStream(Directory.GetCurrentDirectory() + "/output.pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite);
{
    var document = new Document();

    for (var i = 0; i < 4; i++)
    {
        var page = document.Pages.Add();
        Table table = new Table();
        // 设置表格的列宽、边框、填充等属性
        table.ColumnWidths = "50 100 100";
        table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black);
        table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);
        for (var r = 0; r < 20; r++)
        {
            // 创建一行
            Row row = table.Rows.Add();
            // 在这行中添加单元格
            row.Cells.Add("单元格 1");
            row.Cells.Add("单元格 2");
            row.Cells.Add("单元格 3");
        }
        page.Paragraphs.Add(table);
        document.Save(fileStream);
    }
    document.Dispose();

}
Console.ReadLine();

