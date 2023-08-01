using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp;


var builder = WebApplication.CreateSlimBuilder(args);
var app = builder.Build();
if (Capabilities.Build.IsCoreBuild)
{
    GlobalFontSettings.FontResolver = new JapaneseFontResolver();

}
app.MapGet("/pdf", () =>
{


    var document = new PdfDocument();
    document.Info.Title = "台牌";
    document.Info.Subject = "台牌";
    var page = document.AddPage();
    page.Size = PageSize.A4;
    var gfx = XGraphics.FromPdfPage(page);
    var width = page.Width;
    var height = page.Height;

    var pen = new XPen(XColors.Gray, 1);
    pen.DashStyle = XDashStyle.Dash; ;
    gfx.DrawRoundedRectangle(pen, 10, 10, width - 20, 250, 20, 20);
    var icons = new List<PayIcon>()
    {
        new PayIcon("dazhong.jpg",50,20,90,90,"")
    };
    foreach (var icon in icons)
    {
        gfx.DrawImage(XImage.FromFile(icon?.IconPath!), icon!.X, icon.Y, icon.Width, icon.Height);
    }
    var font = new XFont("PretendardJP-Light", 12);
    var text = @"  許諾プログラムは、現状有姿で提供されており、許諾プログラムまたは派生プログラムについて、許諾者は一切の明示または黙示の保証（権利の所在、非侵害、商品性、特定目的への適合性を含むがこれに限られません）を行いません。いかなる場合にも、その原因を問わず、契約上の責任か厳格責任か過失その他の不法行為責任かにかかわらず、また事前に通知されたか否かにかかわらず、許諾者は、許諾プログラムまたは派生プログラムのインストール、使用、複製その他の利用または本契約上の権利の行使によって生じた一切の損害（直接・間接・付随的・特別・拡大・懲罰的または結果的損害）（商品またはサービスの代替品の調達、システム障害から生じた損害、現存するデータまたはプログラムの紛失または破損、逸失利益を含むがこれに限られません）について責任を負いません。
  許諾プログラムは、現状有姿で提供されており、許諾プログラムまたは派生プログラムについて、許諾者は一切の明示または黙示の保証（権利の所在、非侵害、商品性、特定目的への適合性を含むがこれに限られません）を行いません。いかなる場合にも、その原因を問わず、契約上の責任か厳格責任か過失その他の不法行為責任かにかかわらず、また事前に通知されたか否かにかかわらず、許諾者は、許諾プログラムまたは派生プログラムのインストール、使用、複製その他の利用または本契約上の権利の行使によって生じた一切の損害（直接・間接・付随的・特別・拡大・懲罰的または結果的損害）（商品またはサービスの代替品の調達、システム障害から生じた損害、現存するデータまたはプログラムの紛失または破損、逸失利益を含むがこれに限られません）について責任を負いません。";

    gfx.DrawStrings(text, font, XBrushes.Black, new XRect(20, 300, width - 20, 30), XStringFormats.TopLeft, 5);
    var fileMemory = new MemoryStream();
    document.Save(fileMemory, false);
    return Results.File(fileMemory, "application/pdf", "test.pdf");

});

app.Run();


public static class DrawStringExt
{
    public static void DrawStrings(this XGraphics xgf, string text, XFont font, XBrush brush, XRect layoutRectangle, XStringFormat format, XUnit split)
    {
        var (list, lineHeight) = GetLines(xgf, font, XBrushes.Black, text, layoutRectangle.Width);
        for (var i = 0; i < list.Count; i++)
        {
            xgf.DrawString(list[i], font, brush, new XRect(layoutRectangle.X, layoutRectangle.Y + i * (lineHeight + split), layoutRectangle.Width, layoutRectangle.Height), format);
        }
    }
    static (List<string>, XUnit) GetLines(XGraphics xgf, XFont font, XBrush brush, string content, XUnit width)
    {
        var list = new List<string>();
        XUnit height = XUnit.Zero;
        foreach (var text in content.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
        {
            var line = "";
            var temLine = "";
            for (var i = 0; i < text.Length; i++)
            {
                temLine += text[i];
                var size = xgf.MeasureString(temLine, font);
                if (size.Width > width)
                {
                    temLine = text[i].ToString();
                    list.Add(line);

                }
                else
                {
                    line = temLine;
                    if (i == text.Length - 1)
                    {
                        height = size.Height;
                        list.Add(line);
                        break;
                    }
                }
            }
        }
        return (list, height);
    }

}

public record PayIcon(string? IconPath, int X, int Y, int Width, int Height,string Category)
{
}

public class JapaneseFontResolver : IFontResolver
{



    public byte[] GetFont(string faceName)
    {
        switch (faceName)
        {
            case "TakaoGothic":
                return LoadFontData("TakaoGothic.ttf");
            case "PretendardJP-Light":
                return LoadFontData("PretendardJP-Light.ttf");
        }
        return null;
    }

    public FontResolverInfo ResolveTypeface(
                string familyName, bool isBold, bool isItalic)
    {

        switch (familyName)
        {
            case "TakaoGothic":
                return new FontResolverInfo("TakaoGothic");
            case "PretendardJP-Light":
                return new FontResolverInfo("TakaoGothic");
        }
        return PlatformFontResolver.ResolveTypeface("Arial", isBold, isItalic);
    }


    private byte[] LoadFontData(string resourceName)
    {
        using (Stream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), resourceName), FileMode.Open, FileAccess.Read))
        {
            if (stream == null)
                throw new ArgumentException("No resource with name " + resourceName);

            int count = (int)stream.Length;
            byte[] data = new byte[count];
            stream.Read(data, 0, count);
            return data;
        }
    }
}


