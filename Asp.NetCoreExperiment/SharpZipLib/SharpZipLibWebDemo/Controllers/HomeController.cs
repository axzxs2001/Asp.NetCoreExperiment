using CsvHelper;
using Dapper;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpZipLibWebDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        async Task<List<MallGoodsDoc>> GetGoodsesAsync()
        {
            using (var db = new SqlConnection(""))
            {
                var goodses = await db.QueryAsync<MallGoodsDoc>("select top 1000 * from mallgoodsdoc");
                return goodses.ToList();
            }
        }
        [HttpGet("/download")]
        public async Task<ActionResult> DownLoad()
        {
            var zipMemory = new MemoryStream();
            var zipStream = new ZipOutputStream(zipMemory);
            zipStream.SetLevel(3);
            try
            {
                var memory = await GetExcelPackageAsync();
                memory.Flush();
                var excleBuffer = memory.GetBuffer();
                memory.Close();
                var csvName = ZipEntry.CleanName($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xls");
                var newEntry = new ZipEntry(csvName)
                {
                    IsUnicodeText = true,
                    DateTime = DateTime.Now,
                    Size = excleBuffer.Length
                };
                zipStream.PutNextEntry(newEntry);
                await zipStream.WriteAsync(excleBuffer, 0, excleBuffer.Length);
                zipStream.CloseEntry();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            zipStream.IsStreamOwner = true;
            zipMemory.Flush();
            var outBuffer = zipMemory.GetBuffer();

            zipStream.Close();
            zipMemory.Close();
           
            //Array.Reverse(outBuffer);
            //var len = 0;
            //foreach (var b in outBuffer)
            //{
            //    if (b == 0)
            //    {
            //        len++;
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            //Array.Reverse(outBuffer);
            //var newBuffer = outBuffer.Take(outBuffer.Length - len + 128).ToArray();


            var file = File(outBuffer, "application/zip", $"{DateTime.Now.ToString("ddHHmmssfff")}.zip");
            return file;
        }

        (ICellStyle dataStyle, ICellStyle colorStyle) LoadStyle(IWorkbook workbook)
        {
            #region 数据表格
            var dataStyle = workbook.CreateCellStyle();
            //设置内容居中
            dataStyle.VerticalAlignment = VerticalAlignment.Center;
            dataStyle.Alignment = HorizontalAlignment.Center;
            //设置字体
            var datafont = workbook.CreateFont();
            datafont.FontHeightInPoints = 12;
            dataStyle.SetFont(datafont);
            //设置边框
            dataStyle.BorderLeft = BorderStyle.Thin;
            dataStyle.BorderRight = BorderStyle.Thin;
            dataStyle.BorderTop = BorderStyle.Thin;
            dataStyle.BorderBottom = BorderStyle.Thin;
            #endregion           

            #region 颜色字体
            var colorStyle = workbook.CreateCellStyle();
            //设置内容居中
            colorStyle.VerticalAlignment = VerticalAlignment.Center;
            colorStyle.Alignment = HorizontalAlignment.Center;
            //设置字体
            var colorFont = workbook.CreateFont();
            colorFont.FontHeightInPoints = 12;
            colorStyle.SetFont(colorFont);
            colorStyle.FillForegroundColor = HSSFColor.LightOrange.Index;
            colorStyle.FillPattern = FillPattern.SolidForeground;
            colorStyle.BorderLeft = BorderStyle.Thin;
            colorStyle.BorderRight = BorderStyle.Thin;
            colorStyle.BorderTop = BorderStyle.Thin;
            colorStyle.BorderBottom = BorderStyle.Thin;
            #endregion
            return (dataStyle, colorStyle);
        }
        async Task<MemoryStream> GetExcelPackageAsync()
        {
            var memory = new MemoryStream();
            var workbook = new HSSFWorkbook();
            var style = LoadStyle(workbook);
            var sheet = workbook.CreateSheet("商品列表");

            HSSFPalette palette = workbook.GetCustomPalette(); //调色板实例         
            palette.SetColorAtIndex(11, 252, 228, 214);
            style.colorStyle.FillForegroundColor = 11;
            #region 列表           
            SetCell(workbook, sheet, 0, 0, 2, 2, "编号", style.colorStyle, 300);
            SetCell(workbook, sheet, 0, 0, 3, 3, "名称", style.colorStyle);
            SetCell(workbook, sheet, 0, 0, 4, 4, "单位", style.colorStyle);
            SetCell(workbook, sheet, 0, 0, 5, 5, "返点", style.colorStyle);
            SetCell(workbook, sheet, 0, 0, 6, 6, "不知道", style.colorStyle);
            SetCell(workbook, sheet, 0, 0, 7, 7, "规格", style.colorStyle);
            SetCell(workbook, sheet, 0, 0, 8, 8, "状态", style.colorStyle);
            SetCell(workbook, sheet, 0, 0, 9, 9, "顺序", style.colorStyle);

            //设置cell宽度
            sheet.SetColumnWidth(3, 9000);
            sheet.SetColumnWidth(7, 5000);

            var rowIndex = 1;
            var goodses = await GetGoodsesAsync();
            foreach (var item in goodses)
            {
                SetCell(workbook, sheet, rowIndex, rowIndex, 2, 2, item.GoodsID, style.dataStyle);
                SetCell(workbook, sheet, rowIndex, rowIndex, 3, 3, item.Name.Trim(), style.dataStyle);
                SetCell(workbook, sheet, rowIndex, rowIndex, 4, 4, item.Unit, style.dataStyle);
                SetCell(workbook, sheet, rowIndex, rowIndex, 5, 5, item.PointSale, style.dataStyle);
                SetCell(workbook, sheet, rowIndex, rowIndex, 6, 6, item.SaleNum, style.dataStyle);
                SetCell(workbook, sheet, rowIndex, rowIndex, 7, 7, item.Spec.Trim(), style.dataStyle);
                SetCell(workbook, sheet, rowIndex, rowIndex, 8, 8, item.Status, style.dataStyle);
                SetCell(workbook, sheet, rowIndex, rowIndex, 9, 9, item.OrderSn, style.dataStyle);
                rowIndex++;
            }
            #endregion
            workbook.Write(memory);
            return memory;
        }

        void SetCell(IWorkbook workbook, ISheet sheet, int r, int tor, int c, int toc, dynamic value, ICellStyle style, short height = 0)
        {
            var row = sheet.GetRow(r) == null ? sheet.CreateRow(r) : sheet.GetRow(r);
            if (height != 0)
            {
                row.Height = height;
            }
            var cell = row.CreateCell(c);
            if (value != null)
            {
                cell.SetCellValue(value);
            }
            cell.CellStyle = style;
        }
    }



    public class MallGoodsDoc
    {
        public int GoodsID
        { get; set; }

        public string ERPID
        { get; set; }

        public string Name
        { get; set; }

        public string Spec
        { get; set; }

        public string Unit
        { get; set; }

        public string Manufacturer
        { get; set; }

        public int TypeID
        { get; set; }

        public int PointSale
        { get; set; }

        public int SaleNum
        { get; set; }

        public int OrderSn
        { get; set; }

        public int Status
        { get; set; }
    }
}
