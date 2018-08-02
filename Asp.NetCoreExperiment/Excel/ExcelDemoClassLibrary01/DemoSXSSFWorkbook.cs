using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.Streaming;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExcelDemoClassLibrary01
{
    public class DemoSXSSFWorkbook
    {
        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime PublishDate { get; set; } = DateTime.Now;
        /// <summary>
        /// Sheet名
        /// </summary>
        public string SheetName
        { get; set; } = "Test";

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        { get; set; } = "代理店手数料精算書";

        /// <summary>
        /// 周期（2018年5月度）
        /// </summary>
        public DateTime Period
        {
            get; set;
        } = DateTime.Now;
        /// <summary>
        /// 出具公司
        /// </summary>
        public string FromCompany { get; set; } = "株式会社 ネットスターズ";
        /// <summary>
        /// 出具部门
        /// </summary>
        public string FromDepartment { get; set; } = "インバウンド事業部";

        /// <summary>
        /// 到达公司
        /// </summary>
        public string ToCompany { get; set; } = "Test";

        /// <summary>
        /// 代理商信息
        /// </summary>
        public AgentMessage AgentMessage { get; set; } = new AgentMessage
        {
            AgentCode = "000000001",
            DemittanceAmount = 12000,
            DemittanceDate = DateTime.Now
        };
        /// <summary>
        /// 代理帐户信息
        /// </summary>
        public AgentAccount AgentAccount { get; set; } = new AgentAccount
        {
            Account = "123456",
            AccountMode = "普通",
            AccountOwner = "张三",
            BankName = "abc",
            BranchBankName = "2#"
        };

        /// <summary>
        /// 数据列表
        /// </summary>
        public List<Item> Data { get; set; } = new List<Item> {
            new Item { Amount=1000, Code="0001", Memo="无", Name="一号", Rate=0.02d, RateAmount=1000, TimeSection="1月"},
            new Item { Amount=1000, Code="0002", Memo="无", Name="一号", Rate=0.02d, RateAmount=1000, TimeSection="1月"},
            new Item { Amount=1000, Code="0003", Memo="无", Name="一号", Rate=0.02d, RateAmount=1000, TimeSection="1月"},
            new Item { Amount=1000, Code="0004", Memo="无", Name="一号", Rate=0.02d, RateAmount=1000, TimeSection="1月"}
        };



        public MemoryStream GetExcelPackage()
        {
            using (var fs = new MemoryStream())
            {

                var workbook = new SXSSFWorkbook();
                workbook.RandomAccessWindowSize = 1000;
                var sheet = workbook.CreateSheet(SheetName) as SXSSFSheet;
               
                for (var r = 0; r < 50 + Data.Count; r++)
                {
                    var newrow = sheet.CreateRow(r);
                    for (var c = 0; c <40; c++)
                    {
                        newrow.CreateCell(c);
                    }
                }

                sheet.DefaultColumnWidth = 4;
                sheet.DisplayGridlines = false;



                //var rang = new CellRangeAddress(0, 1, 1, 10);
                //var merI = sheet.AddMergedRegion(rang);



                //var row = sheet._rows[0];
                //var cell = row.Cells[1];
                //var style = workbook.CreateCellStyle();
                //var font = workbook.CreateFont();
                //font.FontHeightInPoints = 16;
                //style.SetFont(font);

                //style.BorderTop = BorderStyle.Medium;
                //style.BorderLeft = BorderStyle.Medium;
                //style.BorderRight = BorderStyle.Medium;
                //cell.CellStyle = style;
                //cell.SetCellValue(123);
                //RegionUtil.SetBorderBottom(2, rang, sheet, workbook);
                //RegionUtil.SetBorderLeft(2, rang, sheet, workbook);
                //RegionUtil.SetBorderRight(2, rang, sheet, workbook);
                //RegionUtil.SetBorderTop(2, rang, sheet, workbook);

                SetCell(workbook, sheet, 2, 2, 22, 29, $"発行年月日 {PublishDate.ToString("yyyy年MM月dd日")}", 12, false, BorderStyle.None, false, 700);

                SetCell(workbook, sheet, 5, 11, 1, 13, $"{ToCompany}  御中", 10, true, BorderStyle.Medium, true);

                SetCell(workbook, sheet, 5, 5, 17, 29, FromCompany, 12, false, BorderStyle.None, false);


                SetCell(workbook, sheet, 1, 1, 1, 30, FromDepartment, 12, false, BorderStyle.None, true);


                //设置标题
                var titleFont = workbook.CreateFont();
                titleFont.FontHeightInPoints = 16;
                titleFont.Underline = FontUnderlineType.Single;
                SetCell(workbook, sheet, 16, 17, 0, 30, "adfdsf", titleFont, false, BorderStyle.None, true);

                //设置周期
                var periodFont = workbook.CreateFont();
                periodFont.FontHeightInPoints = 16;
                periodFont.Underline = FontUnderlineType.Single;
                SetCell(workbook, sheet, 20, 20, 1, 12, Period.ToString("yyyy年MM月度"), periodFont, false, BorderStyle.None, false);

                #region  代理商信息


                SetCell(workbook, sheet, 22, 23, 1, 4, "代理店コード", 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, 22, 23, 5, 13, AgentMessage.AgentCode, 12, true, BorderStyle.Thin, true);

                SetCell(workbook, sheet, 24, 25, 1, 4, "振込日", 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, 24, 25, 5, 13, AgentMessage.DemittanceDate?.ToString("yyyy年MM月dd日"), 12, true, BorderStyle.Thin, true);

                SetCell(workbook, sheet, 26, 27, 1, 4, "振込金額", 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, 26, 27, 5, 13, AgentMessage.DemittanceAmount.ToString(), 12, true, BorderStyle.Thin, true);

                #endregion

                #region 代理商帐户信息
                SetCell(workbook, sheet, 22, 23, 17, 20, "金融機関名", 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, 22, 23, 21, 29, AgentAccount.BankName, 12, true, BorderStyle.Thin, true);

                SetCell(workbook, sheet, 24, 25, 17, 20, "支店名", 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, 24, 25, 21, 29, AgentAccount.BranchBankName, 12, true, BorderStyle.Thin, true);

                SetCell(workbook, sheet, 26, 27, 17, 20, "口座番号", 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, 26, 27, 21, 22, AgentAccount.AccountMode, 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, 26, 27, 23, 29, AgentAccount.Account, 12, true, BorderStyle.Thin, true);

                SetCell(workbook, sheet, 28, 29, 17, 20, "口座名義", 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, 28, 29, 21, 29, AgentAccount.AccountOwner, 12, true, BorderStyle.Thin, true);
                #endregion

                #region 列表
                SetCell(workbook, sheet, 32, 32, 1, 1, null, 12, true, BorderStyle.Thin, true, null, HSSFColor.Rose.Index);
                SetCell(workbook, sheet, 32, 32, 2, 4, "コード", 12, true, BorderStyle.Thin, true, null, HSSFColor.Rose.Index);
                SetCell(workbook, sheet, 32, 32, 5, 9, "加盟店", 12, true, BorderStyle.Thin, true, null, HSSFColor.Rose.Index);
                SetCell(workbook, sheet, 32, 32, 10, 14, "対象期間", 12, true, BorderStyle.Thin, true, null, HSSFColor.Rose.Index);
                SetCell(workbook, sheet, 32, 32, 15, 18, "決済利用額", 12, true, BorderStyle.Thin, true, null, HSSFColor.Rose.Index);
                SetCell(workbook, sheet, 32, 32, 19, 21, "手数料率", 12, true, BorderStyle.Thin, true, null, HSSFColor.Rose.Index);
                SetCell(workbook, sheet, 32, 32, 22, 25, "手数料額", 12, true, BorderStyle.Thin, true, null, HSSFColor.Rose.Index);
                SetCell(workbook, sheet, 32, 32, 26, 29, "備考", 12, true, BorderStyle.Thin, true, null, HSSFColor.Rose.Index);

                var rowIndex = 33;
                var sn = 1;
                var total = 0m;
                foreach (var item in Data)
                {
                    total += item.RateAmount;
                    SetCell(workbook, sheet, rowIndex, rowIndex, 1, 1, sn++.ToString(), 12, true, BorderStyle.Thin, true);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 2, 4, item.Code, 12, true, BorderStyle.Thin, true);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 5, 9, item.Name, 12, true, BorderStyle.Thin, true);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 10, 14, item.TimeSection, 12, true, BorderStyle.Thin, true);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 15, 18, item.Amount.ToString(), 12, true, BorderStyle.Thin, true);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 19, 21, item.Rate.ToString(), 12, true, BorderStyle.Thin, true);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 22, 25, item.RateAmount.ToString(), 12, true, BorderStyle.Thin, true);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 26, 29, item.Memo, 12, true, BorderStyle.Thin, true);
                    rowIndex++;
                }

                SetCell(workbook, sheet, rowIndex, rowIndex, 1, 1, "計", 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, rowIndex, rowIndex, 2, 4, "", 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, rowIndex, rowIndex, 5, 9, "", 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, rowIndex, rowIndex, 10, 14, "税抜き", 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, rowIndex, rowIndex, 15, 18, "", 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, rowIndex, rowIndex, 19, 21, "", 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, rowIndex, rowIndex, 22, 25, total.ToString(), 12, true, BorderStyle.Thin, true);
                SetCell(workbook, sheet, rowIndex, rowIndex, 26, 29, "", 12, true, BorderStyle.Thin, true);
                #endregion

                workbook.Write(fs);
                return fs;
            }
        }


        void SetCell(IWorkbook workbook, SXSSFSheet sheet, int r, int tor, int c, int toc, dynamic value, IFont font, bool isBorder = false, BorderStyle borderStyle = BorderStyle.Medium, bool isCenter = false, short? rowHeight = null)
        {
            var rang = new CellRangeAddress(r, tor, c, toc);
            sheet.AddMergedRegion(rang);

            var row = sheet._rows[r];
            if (rowHeight.HasValue)
            {
                row.Height = rowHeight.Value;
            }
            var cell = row.Cells[c];
            if (value != null)
            {
                cell.SetCellValue(value);
            }

            //设置边框
            if (isBorder)
            {
                Parallel.For(r, tor + 1, firstR =>
                {
                    var brow = sheet._rows[firstR];
                    var leftCell = brow.Cells[c];
                    var leftStyle = workbook.CreateCellStyle();
                    leftStyle.BorderLeft = borderStyle;
                    leftCell.CellStyle = leftStyle;

                    var rightCell = brow.Cells[toc];
                    var rightStyle = workbook.CreateCellStyle();
                    rightStyle.BorderRight = borderStyle;
                    rightCell.CellStyle = rightStyle;
                });
                Parallel.For(c, toc + 1, firstC =>
                {
                    var topRow = sheet._rows[r];
                    var topCell = topRow.Cells[firstC];
                    var topStyle = workbook.CreateCellStyle();
                    topStyle.BorderTop = borderStyle;
                    if (firstC == c)
                    {
                        topStyle.BorderLeft = borderStyle;
                    }
                    if (firstC == toc)
                    {
                        topStyle.BorderRight = borderStyle;
                    }
                    topCell.CellStyle = topStyle;

                    var bottomRow = sheet._rows[tor];
                    var bottomCell = bottomRow.Cells[firstC];
                    var bottomStyle = workbook.CreateCellStyle();
                    bottomStyle.BorderBottom = borderStyle;
                    if (firstC == c)
                    {
                        bottomStyle.BorderLeft = borderStyle;
                    }
                    if (firstC == toc)
                    {
                        bottomStyle.BorderRight = borderStyle;
                    }
                    bottomCell.CellStyle = bottomStyle;
                });
                //if (r == tor && c == toc)
                //{
                //    var brow = sheet._rows[r];
                //    var sigleCell = brow.Cells[c];
                //    var sigleStyle = workbook.CreateCellStyle();
                //    sigleStyle.BorderLeft = borderStyle;
                //    sigleCell.CellStyle = sigleStyle;
                //}
                //else
                //{

                //for (var firstR = r; firstR <= tor; firstR++)
                //{
                //    var brow = sheet._rows[firstR];
                //    var leftCell = brow.Cells[c];
                //    var leftStyle = workbook.CreateCellStyle();
                //    leftStyle.BorderLeft = borderStyle;
                //    leftCell.CellStyle = leftStyle;

                //    var rightCell = brow.Cells[toc];
                //    var rightStyle = workbook.CreateCellStyle();
                //    rightStyle.BorderRight = borderStyle;
                //    rightCell.CellStyle = rightStyle;
                //}
                //for (var firstC = c; firstC <= toc; firstC++)
                //{
                //    var topRow = sheet._rows[r];
                //    var topCell = topRow.Cells[firstC];
                //    var topStyle = workbook.CreateCellStyle();
                //    topStyle.BorderTop = borderStyle;
                //    if (firstC == c)
                //    {
                //        topStyle.BorderLeft = borderStyle;
                //    }
                //    if (firstC == toc)
                //    {
                //        topStyle.BorderRight = borderStyle;
                //    }
                //    topCell.CellStyle = topStyle;

                //    var bottomRow = sheet._rows[tor];
                //    var bottomCell = bottomRow.Cells[firstC];
                //    var bottomStyle = workbook.CreateCellStyle();
                //    bottomStyle.BorderBottom = borderStyle;
                //    if (firstC == c)
                //    {
                //        bottomStyle.BorderLeft = borderStyle;
                //    }
                //    if (firstC == toc)
                //    {
                //        bottomStyle.BorderRight = borderStyle;
                //    }
                //    bottomCell.CellStyle = bottomStyle;
                //}
                //}
            }

            var style = isBorder ? cell.CellStyle : workbook.CreateCellStyle();
            //设置字体           
            style.SetFont(font);

            //设置内容居中
            if (isCenter)
            {
                style.VerticalAlignment = VerticalAlignment.Center;
                style.Alignment = HorizontalAlignment.Center;
            }
            cell.CellStyle = style;
        }


        void SetCell(IWorkbook workbook, SXSSFSheet sheet, int r, int tor, int c, int toc, dynamic value, short fontSize = 11, bool isBorder = false, BorderStyle borderStyle = BorderStyle.Medium, bool isCenter = false, short? rowHeight = null, short? color = null)
        {
            var rang = new CellRangeAddress(r, tor, c, toc);
            sheet.AddMergedRegion(rang);

            var row = sheet._rows[r];
            if (rowHeight.HasValue)
            {
                row.Height = rowHeight.Value;
            }
            var cell = row.Cells[c];
            if (value != null)
            {
                cell.SetCellValue(value);
            }


            //设置边框
            if (isBorder)
            {
               // if (r == tor && c == toc)
                {
                    //var brow = sheet._rows[r];
                    //var sigleCell = brow.Cells[c];
                    //var sigleStyle = workbook.CreateCellStyle();
                    //sigleStyle.BorderLeft = borderStyle;
                    //sigleCell.CellStyle = sigleStyle;
                }
               // else
                {
                    Parallel.For(r, tor+1, firstR =>
                    {
                        var brow = sheet._rows[firstR];
                        var leftCell = brow.Cells[c];
                        var leftStyle = workbook.CreateCellStyle();
                        leftStyle.BorderLeft = borderStyle;
                        leftCell.CellStyle = leftStyle;

                        var rightCell = brow.Cells[toc];
                        var rightStyle = workbook.CreateCellStyle();
                        rightStyle.BorderRight = borderStyle;
                        rightCell.CellStyle = rightStyle;
                    });
                    //for (var firstR = r; firstR <= tor; firstR++)
                    //{
                    //    var brow = sheet._rows[firstR];
                    //    var leftCell = brow.Cells[c];
                    //    var leftStyle = workbook.CreateCellStyle();
                    //    leftStyle.BorderLeft = borderStyle;
                    //    leftCell.CellStyle = leftStyle;

                    //    var rightCell = brow.Cells[toc];
                    //    var rightStyle = workbook.CreateCellStyle();
                    //    rightStyle.BorderRight = borderStyle;
                    //    rightCell.CellStyle = rightStyle;
                    //}
                    Parallel.For(c, toc+1, firstC =>
                    {
                        var topRow = sheet._rows[r];
                        var topCell = topRow.Cells[firstC];
                        var topStyle = workbook.CreateCellStyle();
                        topStyle.BorderTop = borderStyle;
                        if (firstC == c)
                        {
                            topStyle.BorderLeft = borderStyle;
                        }
                        if (firstC == toc)
                        {
                            topStyle.BorderRight = borderStyle;
                        }
                        topCell.CellStyle = topStyle;

                        var bottomRow = sheet._rows[tor];
                        var bottomCell = bottomRow.Cells[firstC];
                        var bottomStyle = workbook.CreateCellStyle();
                        bottomStyle.BorderBottom = borderStyle;
                        if (firstC == c)
                        {
                            bottomStyle.BorderLeft = borderStyle;
                        }
                        if (firstC == toc)
                        {
                            bottomStyle.BorderRight = borderStyle;
                        }
                        bottomCell.CellStyle = bottomStyle;
                    });

                    //    for (var firstC = c; firstC <= toc; firstC++)
                    //{
                    //    var topRow = sheet._rows[r];
                    //    var topCell = topRow.Cells[firstC];
                    //    var topStyle = workbook.CreateCellStyle();
                    //    topStyle.BorderTop = borderStyle;
                    //    if (firstC == c)
                    //    {
                    //        topStyle.BorderLeft = borderStyle;
                    //    }
                    //    if (firstC == toc)
                    //    {
                    //        topStyle.BorderRight = borderStyle;
                    //    }
                    //    topCell.CellStyle = topStyle;

                    //    var bottomRow = sheet._rows[tor];
                    //    var bottomCell = bottomRow.Cells[firstC];
                    //    var bottomStyle = workbook.CreateCellStyle();
                    //    bottomStyle.BorderBottom = borderStyle;
                    //    if (firstC == c)
                    //    {
                    //        bottomStyle.BorderLeft = borderStyle;
                    //    }
                    //    if (firstC == toc)
                    //    {
                    //        bottomStyle.BorderRight = borderStyle;
                    //    }
                    //    bottomCell.CellStyle = bottomStyle;
                    //}
                }
            }

            var style = isBorder ? cell.CellStyle : workbook.CreateCellStyle();
            //设置颜色
            if (color.HasValue)
            {
                style.FillForegroundColor = color.Value;
                style.FillPattern = FillPattern.SolidForeground;
            }
            //设置字体
            var font = workbook.CreateFont();
            font.FontHeightInPoints = fontSize;
            style.SetFont(font);
            //设置内容居中
            if (isCenter)
            {
                style.VerticalAlignment = VerticalAlignment.Center;
                style.Alignment = HorizontalAlignment.Center;
            }
            cell.CellStyle = style;


        }

    }

}
