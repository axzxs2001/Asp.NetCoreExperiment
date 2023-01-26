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

//下载excel
app.MapGet("/getfile", async (HttpContext context) =>
{
    using var con = new NpgsqlConnection("Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=;Pooling=true;MinPoolSize=1;MaxPoolSize=100;CommandTimeout=300;");
    var data = await con.ExecuteReaderAsync("select outtradeno,recordid,entname,endtime,tradestate,orderamount,detail from wxbsrecord order by recordid limit 1000000;");
    var memoryStream = new MemoryStream(); 
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
    var replaceList = new List<(string OriginalColumnName, string OriginalColumnValue, string ReplaceColumnName, string ReplaceColumnValue)> { new("outtradeno", "02M714CC0307P20200730180609", "detail", "桂素伟") };

    await memoryStream.SaveAsAsync(value: data, configuration: config, func: BuildStreamData);
    memoryStream.Seek(0, SeekOrigin.Begin);
    var fileName = DateTime.Now.ToString("yyyyMMddhhssmm") + ".xlsx";
    var contentType = "application/octet-stream";

    return TypedResults.File(fileStream: memoryStream, contentType: contentType, fileDownloadName: fileName);

    List<KeyValuePair<int, object>> BuildStreamData(IDataReader reader)
    {
        var xIndex = 1;
        var fieldCount = reader.FieldCount;

        var contents = new List<(int xIndex, string columnName, object cellValue)>();
        var replaceRows = new List<(string columnName, object cellValue)>();
        for (int i = 0; i < fieldCount; i++)
        {
            var cellValue = reader.GetValue(i);
            var columnName = reader.GetName(i);
            //处理格式
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
            //找到要替换列
            foreach (var replaceItem in replaceList)
            {
                if (replaceItem.OriginalColumnName == columnName && replaceItem.OriginalColumnValue == cellValue.ToString())
                {
                    replaceRows.Add((replaceItem.ReplaceColumnName, replaceItem.ReplaceColumnValue));
                }
            }

            contents.Add((xIndex, columnName, cellValue));
            xIndex++;
        }
        var returnContents = new List<KeyValuePair<int, object>>();

        foreach (var ele in contents)
        {
            var isReplace = false;
            foreach (var replaceItem in replaceRows)
            {
                if (ele.columnName == replaceItem.columnName)
                {
                    returnContents.Add(new KeyValuePair<int, object>(ele.xIndex, replaceItem.cellValue));
                    isReplace = true;
                    break;
                }
            }
            if (isReplace == false)
            {
                returnContents.Add(new KeyValuePair<int, object>(ele.xIndex, ele.cellValue));
            }
        }
        return returnContents;

    }

});


//处理下载模版excel
app.MapGet("/gettepfile", async (HttpContext context) =>
{
    using var con = new NpgsqlConnection("Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=starpayagenttmp;Pooling=true;MinPoolSize=1;MaxPoolSize=100;CommandTimeout=300;");
    var data = await con.ExecuteReaderAsync("select outtradeno,recordid,entname,endtime,tradestate,orderamount,detail from wxbsrecord order by recordid limit 1000000;");
    var memoryStream = new MemoryStream();
    var templatePath = Directory.GetCurrentDirectory() + "/temp.xlsx";
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
    var replaceList = new List<(string OriginalColumnName, string OriginalColumnValue, string ReplaceColumnName, string ReplaceColumnValue)> { new("outtradeno", "02M714CC0307P20200730180609", "detail", "桂素伟") };

    // await memoryStream.SaveAsAsync(value: data, configuration: config, func: BuildStreamData);

    await memoryStream.SaveAsByTemplateAsync(templatePath: templatePath, value: data, configuration: config, func: BuildStreamData);
    memoryStream.Seek(0, SeekOrigin.Begin);
    var fileName = DateTime.Now.ToString("yyyyMMddhhssmm") + ".xlsx";
    var contentType = "application/octet-stream";

    return TypedResults.File(fileStream: memoryStream, contentType: contentType, fileDownloadName: fileName);



    List<KeyValuePair<int, object>> BuildStreamData(IDataReader reader)
    {
        var xIndex = 1;
        var fieldCount = reader.FieldCount;

        var contents = new List<(int xIndex, string columnName, object cellValue)>();
        var replaceRows = new List<(string columnName, object cellValue)>();
        for (int i = 0; i < fieldCount; i++)
        {
            var cellValue = reader.GetValue(i);
            var columnName = reader.GetName(i);
            //处理格式
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
            //找到要替换列
            foreach (var replaceItem in replaceList)
            {
                if (replaceItem.OriginalColumnName == columnName && replaceItem.OriginalColumnValue == cellValue.ToString())
                {
                    replaceRows.Add((replaceItem.ReplaceColumnName, replaceItem.ReplaceColumnValue));
                }
            }

            contents.Add((xIndex, columnName, cellValue));
            xIndex++;
        }
        var returnContents = new List<KeyValuePair<int, object>>();

        foreach (var ele in contents)
        {
            var isReplace = false;
            foreach (var replaceItem in replaceRows)
            {
                if (ele.columnName == replaceItem.columnName)
                {
                    returnContents.Add(new KeyValuePair<int, object>(ele.xIndex, replaceItem.cellValue));
                    isReplace = true;
                    break;
                }
            }
            if (isReplace == false)
            {
                returnContents.Add(new KeyValuePair<int, object>(ele.xIndex, ele.cellValue));
            }
        }
        return returnContents;

    }

});
app.Run();

