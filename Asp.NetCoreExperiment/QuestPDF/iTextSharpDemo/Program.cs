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

string src = "C:\\GPT\\aaa.pdf";
string dest = "C:\\GPT\\output.pdf";

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