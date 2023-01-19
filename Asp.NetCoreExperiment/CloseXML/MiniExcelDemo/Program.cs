using MiniExcelLibs;
using Npgsql;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MiniExcelLibs.OpenXml;
using MiniExcelLibs.Attributes;
using System.Xml.Linq;
using System.Data;
using MiniExcelLibs.Utils;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/getfile", async (HttpContext context) =>
{
    using var con = new NpgsqlConnection("Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres;Database=***;Pooling=true;MinPoolSize=1;MaxPoolSize=100;CommandTimeout=300;");
    var data = await con.ExecuteReaderAsync("select * from wxbsrecord order by recordid limit 1000000;");
    var memoryStream = new MemoryStream();
    var templatePath = Directory.GetCurrentDirectory() + "/aaa.xlsx";
    var config = new OpenXmlConfiguration()
    {
        DynamicColumns = new DynamicExcelColumn[] {
            new DynamicExcelColumn("outtradeno") {Name="交易号" },
            new DynamicExcelColumn("recordid") {Name="编号" },
            new DynamicExcelColumn("entname") {Name="企业名称" },
            new DynamicExcelColumn("endtime") { Name="时间",  Format = "yyyy/MM/dd" },
            new DynamicExcelColumn("tradestate") { Name = "状态" },
            new DynamicExcelColumn("orderamount") { Name = "金额",Format="D" },
            new DynamicExcelColumn("detail") { Name = "备注" },
        },
        IgnoreTemplateParameterMissing = false,
    };
    // await memoryStream.SaveAsAsync(value: data, configuration: config, replaceDictionary: replaceDictionary);
    var formats = new Dictionary<string, string> { { "endtime", "yyyy/MM/dd" }, { "orderamount", "000,000" } };
    var replaceDictionary = new Dictionary<string, KeyValuePair<string, string>> { { "02M714CC0307P20200730180609", new KeyValuePair<string, string>("detail", "桂素伟") } };

        await memoryStream.SaveAsAsync(value: data, configuration: config, action: NewMethod);
    memoryStream.Seek(0, SeekOrigin.Begin);
    var fileName = DateTime.Now.ToString("yyyyMMddhhssmm") + ".xlsx";
    var contentType = "application/octet-stream";

    return TypedResults.File(fileStream: memoryStream, contentType: contentType, fileDownloadName: fileName);

    void NewMethod(MiniExcelStreamWriter writer, IDataReader reader, int yIndex, Action<MiniExcelStreamWriter, int, int, object, ExcelColumnInfo> WriteCell)
    {
        var xIndex = 1;
        var fieldCount = reader.FieldCount;
        var contents = new Dictionary<int, KeyValuePair<string, object>>();
        KeyValuePair<string, string>? replaceBody = null;
        for (int i = 0; i < fieldCount; i++)
        {
            var cellValue = reader.GetValue(i);
            var columnName = reader.GetName(i);
            if (formats.ContainsKey(columnName.ToLower()))
            {
                var format = formats[columnName.ToLower()];
                if (cellValue is DateTime)
                {
                    cellValue = Convert.ToDateTime(cellValue).ToString(format);
                }
                else
                {
                    cellValue = Convert.ToInt64(cellValue).ToString(format);
                }
            }
            if (replaceDictionary.ContainsKey(cellValue.ToString()))
            {
                replaceBody = replaceDictionary[cellValue.ToString()];
            }
            contents.Add(xIndex, new KeyValuePair<string, object>(columnName, cellValue));
            xIndex++;
        }
        foreach (var kv in contents)
        {
            if (replaceBody.HasValue && kv.Value.Key == replaceBody.Value.Key)
            {
                WriteCell(writer, yIndex, kv.Key, replaceBody.Value.Value, null);
            }
            else
            {
                WriteCell(writer, yIndex, kv.Key, kv.Value.Value, null);
            }
        }
    }

});
app.Run();

