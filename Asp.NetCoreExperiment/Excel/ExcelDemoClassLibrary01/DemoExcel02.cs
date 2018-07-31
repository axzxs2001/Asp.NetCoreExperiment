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

namespace ExcelDemoClassLibrary01
{
    public class DomoExcel02
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

                var workbook = new XSSFWorkbook();
                var sheet = workbook.CreateSheet(SheetName);
                sheet.DefaultColumnWidth = 3;
                sheet.DisplayGridlines = false;

                sheet.AddMergedRegion(new CellRangeAddress(2, 2, 22, 29));
                var rowIndex = 2;
                IRow row = sheet.CreateRow(rowIndex);
                row.Height = 700;
                ICell cell = row.CreateCell(22);
                cell.SetCellValue($"発行年月日 {PublishDate.ToString("yyyy年MM月dd日")}");
                var font = workbook.CreateFont();
                font.FontHeightInPoints = 12;
                var style = workbook.CreateCellStyle();
                style.SetFont(font);
                cell.CellStyle = style;


                var rang = new CellRangeAddress(5, 11, 1, 13);
                sheet.AddMergedRegion(rang);

                rowIndex = 5;
                var nrow = sheet.CreateRow(rowIndex);
                var ncell = nrow.CreateCell(13);
                ncell.SetCellValue($"{ToCompany}  御中");
                var nfont = workbook.CreateFont();
                nfont.FontHeightInPoints = 10;
                var nstyle = workbook.CreateCellStyle();
                nstyle.SetFont(nfont);
                nstyle.BorderLeft = BorderStyle.Medium;
                nstyle.BorderRight = BorderStyle.Medium;
                nstyle.BorderTop = BorderStyle.Medium;
                nstyle.BorderBottom = BorderStyle.Medium;
                //ncell.CellStyle = nstyle;
                for (int i = rang.FirstRow; i <= rang.LastRow; i++)
                {
                   
                    var row1 = CellUtil.GetRow(i, sheet);
                    for (int j = rang.FirstColumn; j <= rang.LastColumn; j++)
                    {
                        var singleCell = CellUtil.GetCell(row1, (short)j);
                        singleCell.CellStyle = nstyle;
                    }
                }


                workbook.Write(fs);
                return fs;
            }
        }

    }

}
