using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Razor.TagHelpers;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
var app = builder.Build();


app.MapGet("/getfile", async (IHttpClientFactory clientFactory, CancellationToken token) =>
{
    var client = clientFactory.CreateClient();
    var sql = "select field1,field2,field3,field4,field5,field6,field7,field8,field9,field10 from tablename order by field1 limit 1000000";
    var stream = await client.GetStreamAsync($"http://127.0.0.1:8123/?user=dev_owner&password=mypassword&query={sql} FORMAT JSONCompactEachRowWithNamesAndTypes", token);


    var utf8encoding = new UTF8Encoding(true);
    var bufferSize = 5 * 1024 * 1024;
    var memoryStream = new MemoryStream();
    var archive = new ExcelZipArchive(memoryStream, ZipArchiveMode.Create, true, utf8encoding);
    var zipDictionary = new Dictionary<string, ZipPackageInfo>();
    var id = $"R{Guid.NewGuid():N}";
    var sheetName = "Sheet1";
    var sheetPath = $"xl/worksheets/sheet1.xml";
    var sheetIdx = 1;

    #region zip组装
    {
        var _defaultRels = ReplaceString(@"<?xml version=""1.0"" encoding=""utf-8""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
    <Relationship Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"" Target=""xl/workbook.xml"" Id=""Rfc2254092b6248a9"" />
</Relationships>");
        CreateZipEntry("_rels/.rels", "application/vnd.openxmlformats-package.relationships+xml", _defaultRels);

    }
    {
        var _defaultSharedString = ReplaceString("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\" ?><sst xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" count=\"0\" uniqueCount=\"0\"></sst>");

        CreateZipEntry("xl/sharedStrings.xml", "application/vnd.openxmlformats-package.relationships+xml", _defaultSharedString);
    }
    {
        var entry = archive.CreateEntry(sheetPath, CompressionLevel.Fastest);
        using var zipStream = entry.Open();
        using var writer = new StreamWriter(zipStream, utf8encoding, bufferSize);

        writer.Write($@"<?xml version=""1.0"" encoding=""utf-8""?><x:worksheet xmlns:x=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">");
        var yIndex = 1;
        var xIndex = 1;
        {

            var sReader = new StreamReader(stream);
            if (!sReader.EndOfStream)
            {
                var nameLine = await sReader.ReadLineAsync();
                var typeLine = await sReader.ReadLineAsync();
                var fieldNameArr = System.Text.Json.JsonSerializer.Deserialize<object[]>(nameLine);
                int fieldCount = fieldNameArr.Length;
                //处理列宽度

                writer.Write($@"<x:cols>");
                for (int i = 0; i < fieldCount; i++)
                {
                    writer.Write($@"<x:col min=""{i + 1}"" max=""{i + 1}"" {$@"width=""30"""} customWidth=""1"" />");
                }
                writer.Write($@"</x:cols>");

                //处理数据
                writer.Write("<x:sheetData>");
                //处理表头
                writer.Write($"<x:row r=\"{yIndex}\">");
                xIndex = 1;
                for (int i = 0; i < fieldCount; i++)
                {
                    var columnName = fieldNameArr[i].ToString();
                    WriteC(writer, "1", columnName);
                    xIndex++;
                }
                writer.Write($"</x:row>");
                yIndex++;
            }


            //处理数据
            while (!sReader.EndOfStream)
            {
                writer.Write($"<x:row r=\"{yIndex}\">");
                var dataLine = await sReader.ReadLineAsync();
                var dataArr = System.Text.Json.JsonSerializer.Deserialize<object[]>(dataLine);
                xIndex = 1;
                for (var i = 0; i < dataArr.Length; i++)
                {
                    WriteCell(writer, yIndex, xIndex, dataArr[i]);
                    xIndex++;
                }
                writer.Write($"</x:row>");
                yIndex++;
            }
        }
        writer.Write("</x:sheetData>");
        writer.Write("</x:worksheet>");
        writer.Flush();
    }
    {
        string _defaultStylesXml = ReplaceString(@"<?xml version=""1.0"" encoding=""utf-8""?>
<x:styleSheet xmlns:x=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">
    <x:numFmts count=""1"">
        <x:numFmt numFmtId=""0"" formatCode="""" />
    </x:numFmts>
    <x:fonts count=""2"">
        <x:font>
            <x:vertAlign val=""baseline"" />
            <x:sz val=""11"" />
            <x:color rgb=""FF000000"" />
            <x:name val=""Calibri"" />
            <x:family val=""2"" />
        </x:font>
        <x:font>
            <x:vertAlign val=""baseline"" />
            <x:sz val=""11"" />
            <x:color rgb=""00000000"" />
            <x:name val=""Calibri"" />
            <x:family val=""2"" />
        </x:font>
    </x:fonts>
    <x:fills count=""3"">
        <x:fill>
            <x:patternFill patternType=""none"" />
        </x:fill>
        <x:fill>
            <x:patternFill patternType=""gray125"" />
        </x:fill>
        <x:fill>
            <x:patternFill patternType=""solid"">
                <x:fgColor rgb=""FFFFFFFF"" />
            </x:patternFill>
        </x:fill>
    </x:fills>
    <x:borders count=""2"">
        <x:border diagonalUp=""0"" diagonalDown=""0"">
            <x:left style=""none"">
                <x:color rgb=""FF000000"" />
            </x:left>
            <x:right style=""none"">
                <x:color rgb=""FF000000"" />
            </x:right>
            <x:top style=""none"">
                <x:color rgb=""FF000000"" />
            </x:top>
            <x:bottom style=""none"">
                <x:color rgb=""FF000000"" />
            </x:bottom>
            <x:diagonal style=""none"">
                <x:color rgb=""FF000000"" />
            </x:diagonal>
        </x:border>
        <x:border diagonalUp=""0"" diagonalDown=""0"">
            <x:left style=""thin"">
                <x:color rgb=""FF000000"" />
            </x:left>
            <x:right style=""thin"">
                <x:color rgb=""FF000000"" />
            </x:right>
            <x:top style=""thin"">
                <x:color rgb=""FF000000"" />
            </x:top>
            <x:bottom style=""thin"">
                <x:color rgb=""FF000000"" />
            </x:bottom>
            <x:diagonal style=""none"">
                <x:color rgb=""FF000000"" />
            </x:diagonal>
        </x:border>
    </x:borders>
    <x:cellStyleXfs count=""4"">
        <x:xf numFmtId=""0"" fontId=""0"" fillId=""0"" borderId=""0"" applyNumberFormat=""1"" applyFill=""1"" applyBorder=""0"" applyAlignment=""1"" applyProtection=""1"">
            <x:protection locked=""1"" hidden=""0"" />
        </x:xf>
        <x:xf numFmtId=""14"" fontId=""1"" fillId=""2"" borderId=""1"" applyNumberFormat=""1"" applyFill=""0"" applyBorder=""1"" applyAlignment=""1"" applyProtection=""1"">
            <x:protection locked=""1"" hidden=""0"" />
        </x:xf>
        <x:xf numFmtId=""0"" fontId=""0"" fillId=""0"" borderId=""1"" applyNumberFormat=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1"" applyProtection=""1"">
            <x:protection locked=""1"" hidden=""0"" />
        </x:xf>
        <x:xf numFmtId=""3"" fontId=""0"" fillId=""0"" borderId=""1"" applyNumberFormat=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1"" applyProtection=""1"">
        <x:protection locked=""1"" hidden=""0""/>
    </x:xf>
    </x:cellStyleXfs>
    <x:cellXfs count=""5"">
        <x:xf></x:xf>
        <x:xf numFmtId=""0"" fontId=""1"" fillId=""2"" borderId=""1"" xfId=""0"" applyNumberFormat=""1"" applyFill=""0"" applyBorder=""1"" applyAlignment=""1"" applyProtection=""1"">
            <x:alignment horizontal=""left"" vertical=""bottom"" textRotation=""0"" wrapText=""0"" indent=""0"" relativeIndent=""0"" justifyLastLine=""0"" shrinkToFit=""0"" readingOrder=""0"" />
            <x:protection locked=""1"" hidden=""0"" />
        </x:xf>
        <x:xf numFmtId=""0"" fontId=""0"" fillId=""0"" borderId=""1"" xfId=""0"" applyNumberFormat=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1"" applyProtection=""1"">
            <x:alignment horizontal=""general"" vertical=""bottom"" textRotation=""0"" wrapText=""0"" indent=""0"" relativeIndent=""0"" justifyLastLine=""0"" shrinkToFit=""0"" readingOrder=""0"" />
            <x:protection locked=""1"" hidden=""0"" />
        </x:xf>
        <x:xf numFmtId=""14"" fontId=""0"" fillId=""0"" borderId=""1"" xfId=""0"" applyNumberFormat=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1"" applyProtection=""1"">
            <x:alignment horizontal=""general"" vertical=""bottom"" textRotation=""0"" wrapText=""0"" indent=""0"" relativeIndent=""0"" justifyLastLine=""0"" shrinkToFit=""0"" readingOrder=""0"" />
            <x:protection locked=""1"" hidden=""0"" />
        </x:xf>
        <x:xf numFmtId=""0"" fontId=""0"" fillId=""0"" borderId=""1"" xfId=""0"" applyBorder=""1"" applyAlignment=""1"">
            <x:alignment horizontal=""fill""/>
        </x:xf>
        <x:xf numFmtId=""3"" fontId=""0"" fillId=""0"" borderId=""1"" xfId=""0"" applyNumberFormat=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1"" applyProtection=""1"">
             <x:alignment horizontal=""center"" vertical=""center"" textRotation=""0"" wrapText=""0"" indent=""0"" relativeIndent=""0"" justifyLastLine=""0"" shrinkToFit=""0"" readingOrder=""0""/>
             <x:protection locked=""1"" hidden=""0""/>
         </x:xf>
    </x:cellXfs>
    <x:cellStyles count=""1"">
        <x:cellStyle name=""Normal"" xfId=""0"" builtinId=""0"" />
    </x:cellStyles>
</x:styleSheet>");
        var styleXml = _defaultStylesXml;
        CreateZipEntry(@"xl/styles.xml", "application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml", styleXml);
    }
    {
        string _defaultDrawingXmlRels = ReplaceString(@"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
    {{format}}
</Relationships>");
        var drawing = new StringBuilder();
        CreateZipEntry($"xl/drawings/_rels/drawing1.xml.rels", "",
            _defaultDrawingXmlRels.Replace("{{format}}", drawing.ToString()));
    }
    {
        var drawing = new StringBuilder();
        string _defaultDrawing = ReplaceString(@"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
<xdr:wsDr xmlns:a=""http://schemas.openxmlformats.org/drawingml/2006/main""
    xmlns:r=""http://schemas.openxmlformats.org/officeDocument/2006/relationships""
    xmlns:xdr=""http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing"">
    {{format}}
</xdr:wsDr>");
        CreateZipEntry($"xl/drawings/drawing1.xml", "application/vnd.openxmlformats-officedocument.drawing+xml",
                     _defaultDrawing.Replace("{{format}}", drawing.ToString()));
    }
    {
        var workbookXml = new StringBuilder();
        var workbookRelsXml = new StringBuilder();
        var sheetId = 1;
        workbookXml.AppendLine($@"<x:sheet name=""{sheetName}"" sheetId=""{sheetId}"" r:id=""{id}"" />");
        workbookRelsXml.AppendLine($@"<Relationship Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""/{sheetPath}"" Id=""{id}"" />");
        var sheetRelsXml = ReplaceString($@"<Relationship Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing"" Target=""../drawings/drawing{sheetId}.xml"" Id=""drawing{sheetId}"" />");
        string _defaultSheetRelXml = ReplaceString(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
    {{format}}
</Relationships>");
        CreateZipEntry($"xl/worksheets/_rels/sheet{sheetIdx}.xml.rels", "",
                _defaultSheetRelXml.Replace("{{format}}", sheetRelsXml));
        string _defaultWorkbookXml = ReplaceString(@"<?xml version=""1.0"" encoding=""utf-8""?>
<x:workbook xmlns:r=""http://schemas.openxmlformats.org/officeDocument/2006/relationships""
    xmlns:x=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">
    <x:sheets>
        {{sheets}}
    </x:sheets>
</x:workbook>");
        CreateZipEntry(@"xl/workbook.xml", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml",
            _defaultWorkbookXml.Replace("{{sheets}}", workbookXml.ToString()));
        string _defaultWorkbookXmlRels = ReplaceString(@"<?xml version=""1.0"" encoding=""utf-8""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
    {{sheets}}
    <Relationship Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles"" Target=""/xl/styles.xml"" Id=""R3db9602ace774fdb"" />
</Relationships>");
        CreateZipEntry(@"xl/_rels/workbook.xml.rels", "",
            _defaultWorkbookXmlRels.Replace("{{sheets}}", workbookRelsXml.ToString()));
        var sb = new StringBuilder(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?><Types xmlns=""http://schemas.openxmlformats.org/package/2006/content-types""><Default ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.printerSettings"" Extension=""bin""/><Default ContentType=""application/xml"" Extension=""xml""/><Default ContentType=""image/jpeg"" Extension=""jpg""/><Default ContentType=""image/png"" Extension=""png""/><Default ContentType=""image/gif"" Extension=""gif""/><Default ContentType=""application/vnd.openxmlformats-package.relationships+xml"" Extension=""rels""/>");
        foreach (var p in zipDictionary)
        {
            sb.Append($"<Override ContentType=\"{p.Value.ContentType}\" PartName=\"/{p.Key}\" />");
        }
        sb.Append("</Types>");
        var entry = archive.CreateEntry("[Content_Types].xml", CompressionLevel.Fastest);
        using var zipStream = entry.Open();
        using var writer = new StreamWriter(zipStream, utf8encoding, bufferSize);
        writer.Write(sb.ToString());
    }
    #endregion
    archive.Dispose();
    void CreateZipEntry(string path, string contentType, string content)
    {
        var entry = archive.CreateEntry(path, CompressionLevel.Fastest);
        using var zipStream = entry.Open();
        using var writer = new StreamWriter(zipStream, utf8encoding, bufferSize);
        writer.Write(content);
        if (!string.IsNullOrEmpty(contentType))
            zipDictionary.Add(path, new ZipPackageInfo(entry, contentType));
    }
    memoryStream.Seek(0, SeekOrigin.Begin);
    await memoryStream.FlushAsync(token);
    return TypedResults.File(fileStream: memoryStream, contentType: "application/octet-stream", fileDownloadName: $"{DateTime.Now.ToString("yyyyMMddhhssmm")}.xlsx");

});
app.Run();

string ReplaceString(string xml) => xml.Replace("\r", "").Replace("\n", "").Replace("\t", "");

string ConvertXyToCell(int x, int y)
{
    int dividend = x;
    string columnName = String.Empty;
    int modulo;

    while (dividend > 0)
    {
        modulo = (dividend - 1) % 26;
        columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
        dividend = (int)((dividend - modulo) / 26);
    }
    return $"{columnName}{y}";
}
void WriteC(StreamWriter writer, string r, string columnName)
{
    writer.Write($"<x:c r=\"{r}\" t=\"str\" s=\"1\">");
    writer.Write($"<x:v>{EncodeXML(columnName)}"); //issue I45TF5
    writer.Write($"</x:v>");
    writer.Write($"</x:c>");
}

void WriteCell(StreamWriter writer, int rowIndex, int cellIndex, object value)
{
    var v = string.Empty;
    var t = "str";
    var s = "2";
    if (value == null)
    {
        v = "";
    }
    else if (value is string str)
    {
        v = EncodeXML(str);
    }

    else
    {
        var type = value.GetType();
        type = Nullable.GetUnderlyingType(type) ?? type;


        if (IsNumericType(type))
        {
            t = "n";
            //if (isMoney)
            //{
            //    s = "5";
            //}

            if (type.IsAssignableFrom(typeof(decimal)))
                v = ((decimal)value).ToString(CultureInfo.InvariantCulture);
            else if (type.IsAssignableFrom(typeof(Int32)))
                v = ((Int32)value).ToString(CultureInfo.InvariantCulture);
            else if (type.IsAssignableFrom(typeof(Double)))
                v = ((Double)value).ToString(CultureInfo.InvariantCulture);
            else if (type.IsAssignableFrom(typeof(Int64)))
                v = ((Int64)value).ToString(CultureInfo.InvariantCulture);
            else if (type.IsAssignableFrom(typeof(UInt32)))
                v = ((UInt32)value).ToString(CultureInfo.InvariantCulture);
            else if (type.IsAssignableFrom(typeof(UInt16)))
                v = ((UInt16)value).ToString(CultureInfo.InvariantCulture);
            else if (type.IsAssignableFrom(typeof(UInt64)))
                v = ((UInt64)value).ToString(CultureInfo.InvariantCulture);
            else if (type.IsAssignableFrom(typeof(Int16)))
                v = ((Int16)value).ToString(CultureInfo.InvariantCulture);
            else if (type.IsAssignableFrom(typeof(Single)))
                v = ((Single)value).ToString(CultureInfo.InvariantCulture);
            else if (type.IsAssignableFrom(typeof(Single)))
                v = ((Single)value).ToString(CultureInfo.InvariantCulture);
            else
                v = (decimal.Parse(value.ToString())).ToString(CultureInfo.InvariantCulture);
        }
        else if (type == typeof(bool))
        {
            t = "b";
            v = (bool)value ? "1" : "0";
        }

        else if (type == typeof(DateTime))
        {
            t = null;
            s = "3";
            v = ((DateTime)value).ToOADate().ToString(CultureInfo.InvariantCulture);
        }
        else
        {
            v = EncodeXML(value.ToString());
        }
    }

    var columname = ConvertXyToCell(cellIndex, rowIndex);
    if (v != null && (v.StartsWith(" ", StringComparison.Ordinal) || v.EndsWith(" ", StringComparison.Ordinal))) /*Prefix and suffix blank space will lost after SaveAs #294*/
        writer.Write($"<x:c r=\"{columname}\" {(t == null ? "" : $"t =\"{t}\"")} s=\"{s}\" xml:space=\"preserve\"><x:v>{v}</x:v></x:c>");
    else
        //t check avoid format error ![image](https://user-images.githubusercontent.com/12729184/118770190-9eee3480-b8b3-11eb-9f5a-87a439f5e320.png)
        writer.Write($"<x:c r=\"{columname}\" {(t == null ? "" : $"t =\"{t}\"")} s=\"{s}\"><x:v>{v}</x:v></x:c>");
}
bool IsNumericType(Type type, bool isNullableUnderlyingType = false)
{
    if (isNullableUnderlyingType)
        type = Nullable.GetUnderlyingType(type) ?? type;
    switch (Type.GetTypeCode(type))
    {
        //case TypeCode.Byte:
        //case TypeCode.SByte:
        case TypeCode.UInt16:
        case TypeCode.UInt32:
        case TypeCode.UInt64:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.Decimal:
        case TypeCode.Double:
        case TypeCode.Single:
            return true;
        default:
            return false;
    }
}
string EncodeXML(string value) => value == null
                ? string.Empty
                : XmlEncoder.EncodeString(value)
                            .Replace("&", "&amp;")
                            .Replace("<", "&lt;")
                            .Replace(">", "&gt;")
                            .Replace("\"", "&quot;")
                            .Replace("'", "&apos;")
                            .ToString();
public class ExcelZipArchive : ZipArchive
{
    public ExcelZipArchive(Stream stream, ZipArchiveMode mode, bool leaveOpen, Encoding entryNameEncoding)
       : base(stream, mode, leaveOpen, entryNameEncoding)
    {
    }

    public new void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
class XmlEncoder
{
    private static readonly Regex xHHHHRegex = new Regex("_(x[\\dA-Fa-f]{4})_", RegexOptions.Compiled);
    private static readonly Regex Uppercase_X_HHHHRegex = new Regex("_(X[\\dA-Fa-f]{4})_", RegexOptions.Compiled);

    public static StringBuilder EncodeString(string encodeStr)
    {
        if (encodeStr == null) return null;

        encodeStr = xHHHHRegex.Replace(encodeStr, "_x005F_$1_");

        var sb = new StringBuilder(encodeStr.Length);

        foreach (var ch in encodeStr)
        {
            if (XmlConvert.IsXmlChar(ch))
                sb.Append(ch);
            else
                sb.Append(XmlConvert.EncodeName(ch.ToString()));
        }

        return sb;
    }

    public static string DecodeString(string decodeStr)
    {
        if (string.IsNullOrEmpty(decodeStr))
            return string.Empty;
        decodeStr = Uppercase_X_HHHHRegex.Replace(decodeStr, "_x005F_$1_");
        return XmlConvert.DecodeName(decodeStr);
    }


    private static readonly Regex EscapeRegex = new Regex("_x([0-9A-F]{4,4})_");
    public static string ConvertEscapeChars(string input)
    {
        return EscapeRegex.Replace(input, m => ((char)uint.Parse(m.Groups[1].Value, NumberStyles.HexNumber)).ToString());
    }
}

internal class ZipPackageInfo
{
    public ZipArchiveEntry ZipArchiveEntry { get; set; }
    public string ContentType { get; set; }
    public ZipPackageInfo(ZipArchiveEntry zipArchiveEntry, string contentType)
    {
        this.ZipArchiveEntry = zipArchiveEntry;
        ContentType = contentType;
    }
}