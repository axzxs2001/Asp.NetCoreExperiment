using iText.Forms.Fields;
using iText.Forms;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText;
using iText.Kernel.Pdf.Canvas.Parser.Data;

string src = "C:\\GPT\\Form_Template.pdf";
string dest = "C:\\GPT\\output.pdf";

WritePDF();
//ExtractObjectsFromPage();

//填写表单数据
void WritePDF()
{
    var inputPdfPath = "C:\\GPT\\test01.pdf";
    var outputPdfPath = "C:\\GPT\\test01_result.pdf";
    using (PdfReader pdfReader = new PdfReader(inputPdfPath))
    using (PdfWriter pdfWriter = new PdfWriter(outputPdfPath))
    using (PdfDocument pdfDoc = new PdfDocument(pdfReader, pdfWriter))
    {
        // 获取PDF中的表单对象
        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
        // 获取所有的表单字段
        IDictionary<string, PdfFormField> fields = form.GetAllFormFields();
        // 输出所有的表单字段名称（用于调试）
        foreach (var field in fields)
        {
            fields[field.Key].SetValue("Test");
            Console.WriteLine("Field Name: " + field.Key);
        }      
        // 可选：将表单平面化以防止进一步编辑
        form.FlattenFields();
        // 保存并关闭PDF文件
        pdfDoc.Close();
    }
}

void ExtractObjectsFromPage(string filePath = "C:\\GPT\\bbb.pdf", int pageNumber = 1)
{
    // 打开 PDF 文档
    using (PdfReader pdfReader = new PdfReader(filePath))
    using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
    {
        // 创建自定义对象提取策略
        var strategy = new MyObjectExtractionStrategy();
        var processor = new PdfCanvasProcessor(strategy);

        // 处理指定页码的内容
        processor.ProcessPageContent(pdfDocument.GetPage(pageNumber));

        // 输出提取的对象信息
        foreach (var obj in strategy.Objects)
        {
            Console.WriteLine($"Object Type: {obj.ObjectType}");
            if (!string.IsNullOrEmpty(obj.Content))
                Console.WriteLine($"Content: {obj.Content}");
            Console.WriteLine($"Position: (x: {obj.X}, y: {obj.Y}), Size: (width: {obj.Width}, height: {obj.Height})");
            Console.WriteLine("-------------");
        }
    }
}



void GetTextPostion()
{
    using (PdfReader pdfReader = new PdfReader(src))
    using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
    {
        for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
        {
            // 使用自定义策略提取文本及其位置信息
            var strategy = new MyLocationTextExtractionStrategy();
            PdfCanvasProcessor parser = new PdfCanvasProcessor(strategy);
            parser.ProcessPageContent(pdfDocument.GetPage(i));

            // 输出提取的文本及其位置
            foreach (var chunk in strategy.TextChunks)
            {
                Console.WriteLine($"Text: {chunk.Text}");
                Console.WriteLine($"Position (x, y): ({chunk.X}, {chunk.Y})");
                Console.WriteLine($"Width: {chunk.Width}");
                Console.WriteLine($"Height: {chunk.Height}");
                Console.WriteLine("-------------");
            }
        }
    }
}

void ReWrite()
{
    // 读取PDF文件
    var reader = new PdfReader(src);
    var writer = new PdfWriter(dest);
    var pdfDoc = new PdfDocument(reader, writer);


    var page = pdfDoc.GetPage(1);
    var pdfCanvas = new PdfCanvas(page);
    var canvas = new Canvas(pdfCanvas, page.GetPageSize());
    var fontPath = "TakaoGothic.ttf";
    var font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

    canvas.SetFont(font);
    var color = ColorConstants.BLUE;

    //加载文字
    var items = new List<Item>
    {
        new Item{ Text="2024    09    07",Left=160,Bottom=784,Width=200, FontSize=10},
    new Item{ Text="中国",Left=172,Bottom=710,Width=100, FontSize=12},
    new Item{ Text="山西黎城",Left=172,Bottom=688,Width=100, FontSize=12},
    new Item{ Text="ミハマシティ検見川浜Ⅱ街区",Left=172,Bottom=666,Width=200, FontSize=12},
    new Item{ Text="2024   05  05",Left=477,Bottom=651,Width=200, FontSize=10},
    new Item{ Text="ケイ",Left=210,Bottom=620,Width=100, FontSize=12},
    new Item{ Text="スイ",Left=340,Bottom=620,Width=100, FontSize=12},
    new Item{ Text="桂",Left=210,Bottom=580,Width=100, FontSize=16},
    new Item{ Text="素偉",Left=340,Bottom=580,Width=100, FontSize=16},
    new Item{ Text="なし",Left=515,Bottom=594,Width=100, FontSize=20},
    new Item{ Text="54    06   22",Left=303,Bottom=559,Width=200, FontSize=10},
    new Item{ Text="1   2   3   4   5    6   7   8   9   0   1   2",Left=216,Bottom=518,Width=400, FontSize=16},
};
    foreach (var item in items)
    {
        var paragraph = new Paragraph(item.Text)
            .SetFont(font)
            .SetFontColor(color)
            .SetFontSize(item.FontSize)
            .SetFixedPosition(item.Left, item.Bottom, item.Width);
        canvas.Add(paragraph);
    }
    string imagePath = "gsw.jpg";


    // 加载图片
    var imageData = ImageDataFactory.Create(imagePath);
    Image image = new Image(imageData);
    image.SetFixedPosition(451.5f, 671);
    image.SetWidth(140);
    image.SetHeight(140);
    canvas.Add(image);


    //加载画图
    pdfCanvas.SetStrokeColor(color);
    pdfCanvas.SetLineWidth(1);
    var circles = new List<Circle>
    {
        new Circle{X=240,Y=567,Radius=7},
        new Circle{X=575,Y=567,Radius=7},
    };
    foreach (var circle in circles)
    {
        pdfCanvas.Circle(circle.X, circle.Y, circle.Radius);
    }
    pdfCanvas.Stroke();

    canvas.Close();
    pdfDoc.Close();
}
class Item
{
    public string Text { get; set; }
    public float Left { get; set; }
    public float Bottom { get; set; }
    public float Width { get; set; }
    public float FontSize { get; set; }
}
class Circle
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Radius { get; set; }
}


public class MyLocationTextExtractionStrategy : IEventListener
{
    public List<TextChunk> TextChunks { get; } = new List<TextChunk>();

    public void EventOccurred(IEventData data, EventType type)
    {
        if (type == EventType.RENDER_TEXT)
        {
            var renderInfo = (TextRenderInfo)data;
            var text = renderInfo.GetText();
            var rectangle = renderInfo.GetDescentLine().GetBoundingRectangle();

            TextChunks.Add(new TextChunk(text, rectangle.GetX(), rectangle.GetY(), rectangle.GetWidth(), rectangle.GetHeight()));
        }
    }

    public ICollection<EventType> GetSupportedEvents()
    {
        return null; // 支持所有事件
    }

    public class TextChunk
    {
        public string Text { get; }
        public float X { get; }
        public float Y { get; }
        public float Width { get; }
        public float Height { get; }

        public TextChunk(string text, float x, float y, float width, float height)
        {
            Text = text;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}

public class MyObjectExtractionStrategy : IEventListener
{
    // 存储提取的对象信息
    public List<PdfObjectInfo> Objects { get; } = new List<PdfObjectInfo>();

    // 当事件发生时（例如，渲染文本、图像、路径等）
    public void EventOccurred(IEventData data, EventType type)
    {
        switch (type)
        {
            case EventType.RENDER_TEXT:
                var textRenderInfo = (TextRenderInfo)data;
                var textRect = textRenderInfo.GetDescentLine().GetBoundingRectangle();
                Objects.Add(new PdfObjectInfo
                {
                    ObjectType = "Text",
                    Content = textRenderInfo.GetText(),
                    X = (float)textRect.GetX(),
                    Y = (float)textRect.GetY(),
                    Width = (float)textRect.GetWidth(),
                    Height = (float)textRect.GetHeight()
                });
                break;

            case EventType.RENDER_IMAGE:
                var imageRenderInfo = (ImageRenderInfo)data;
                var imageRect = imageRenderInfo.GetImageCtm();

                Objects.Add(new PdfObjectInfo
                {
                    ObjectType = "Image",
                    X = (float)imageRect.Get(6),
                    Y = (float)imageRect.Get(7),
                    Width = (float)imageRect.Get(0),
                    Height = (float)imageRect.Get(4)
                });
                break;

            case EventType.RENDER_PATH:
                var pathRenderInfo = (PathRenderInfo)data;
                var path = pathRenderInfo.GetPath();
                var point = path.GetCurrentPoint();
                Objects.Add(new PdfObjectInfo
                {
                    ObjectType = "Path",
                    X = (float)point.GetX(),
                    Y = (float)point.GetY(),
                    //Width = (float)pathRect.GetWidth(),
                    //Height = (float)pathRect.GetHeight()
                });
                break;

            default:
                break;
        }
    }

    // 获取支持的事件类型
    public ICollection<EventType> GetSupportedEvents()
    {
        return null; // 支持所有事件
    }
}

// PDF对象信息类
public class PdfObjectInfo
{
    public string ObjectType { get; set; }
    public string Content { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
}
