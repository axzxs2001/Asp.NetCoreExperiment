using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.Streaming;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace ExcelDemoClassLibrary01
{
    public class DemoHSSFWorkbook
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


        ICellStyle _dataStyle, _colotStyle;
        ICellStyle _style12, _style11,_style14,_style16, _style10, _styleTotal;
        ICellStyle _styleTopLeft, _styleTopRight, _styleRight, _styleLeft, _styleBottomLeft, _styleBottomRight;
        void LoadStyle(IWorkbook workbook)
        {
            #region 数据表格
            _dataStyle = workbook.CreateCellStyle();
            //设置内容居中
            _dataStyle.VerticalAlignment = VerticalAlignment.Center;
            _dataStyle.Alignment = HorizontalAlignment.Center;
            //设置字体
            var datafont = workbook.CreateFont();
            datafont.FontHeightInPoints = 12;
            _dataStyle.SetFont(datafont);
            //设置边框
            _dataStyle.BorderLeft = BorderStyle.Thin;
            _dataStyle.BorderRight = BorderStyle.Thin;
            _dataStyle.BorderTop = BorderStyle.Thin;
            _dataStyle.BorderBottom = BorderStyle.Thin;
            #endregion

            #region 上左
            _styleTopLeft = workbook.CreateCellStyle();
            //设置内容居中
            _styleTopLeft.VerticalAlignment = VerticalAlignment.Center;
            _styleTopLeft.Alignment = HorizontalAlignment.Center;
            //设置字体
            var topleftfont = workbook.CreateFont();
            topleftfont.FontHeightInPoints = 12;
            _styleTopLeft.SetFont(topleftfont);
            //设置边框
            _styleTopLeft.BorderLeft = BorderStyle.Medium;
            _styleTopLeft.BorderRight = BorderStyle.Thin;
            _styleTopLeft.BorderTop = BorderStyle.Medium;
            _styleTopLeft.BorderBottom = BorderStyle.Thin;
            #endregion

            #region 上右
            _styleTopRight = workbook.CreateCellStyle();
            //设置内容居中
            _styleTopRight.VerticalAlignment = VerticalAlignment.Center;
            _styleTopRight.Alignment = HorizontalAlignment.Center;
            //设置字体
            var toprightfont = workbook.CreateFont();
            toprightfont.FontHeightInPoints = 12;
            _styleTopRight.SetFont(toprightfont);
            //设置边框
            _styleTopRight.BorderLeft = BorderStyle.Thin;
            _styleTopRight.BorderRight = BorderStyle.Medium;
            _styleTopRight.BorderTop = BorderStyle.Medium;
            _styleTopRight.BorderBottom = BorderStyle.Thin;
            #endregion

            #region 下左
            _styleBottomLeft = workbook.CreateCellStyle();
            //设置内容居中
            _styleBottomLeft.VerticalAlignment = VerticalAlignment.Center;
            _styleBottomLeft.Alignment = HorizontalAlignment.Center;
            //设置字体
            var bottomleftfont = workbook.CreateFont();
            bottomleftfont.FontHeightInPoints = 12;
            _styleBottomLeft.SetFont(bottomleftfont);
            //设置边框
            _styleBottomLeft.BorderLeft = BorderStyle.Medium;
            _styleBottomLeft.BorderRight = BorderStyle.Thin;
            _styleBottomLeft.BorderTop = BorderStyle.Thin;
            _styleBottomLeft.BorderBottom = BorderStyle.Medium;
            #endregion

            #region 下右
            _styleBottomRight = workbook.CreateCellStyle();
            //设置内容居中
            _styleBottomRight.VerticalAlignment = VerticalAlignment.Center;
            _styleBottomRight.Alignment = HorizontalAlignment.Center;
            //设置字体
            var bottomrightfont = workbook.CreateFont();
            bottomrightfont.FontHeightInPoints = 12;
            _styleBottomRight.SetFont(bottomrightfont);
            //设置边框
            _styleBottomRight.BorderLeft = BorderStyle.Thin;
            _styleBottomRight.BorderRight = BorderStyle.Medium;
            _styleBottomRight.BorderTop = BorderStyle.Thin;
            _styleBottomRight.BorderBottom = BorderStyle.Medium;
            #endregion


            #region 左
            _styleLeft = workbook.CreateCellStyle();
            //设置内容居中
            _styleLeft.VerticalAlignment = VerticalAlignment.Center;
            _styleLeft.Alignment = HorizontalAlignment.Center;
            //设置字体
            var leftfont = workbook.CreateFont();
            leftfont.FontHeightInPoints = 12;
            _styleLeft.SetFont(leftfont);
            //设置边框
            _styleLeft.BorderLeft = BorderStyle.Medium;
            _styleLeft.BorderRight = BorderStyle.Thin;
            _styleLeft.BorderTop = BorderStyle.Thin;
            _styleLeft.BorderBottom = BorderStyle.Thin;
            #endregion

            #region 右
            _styleRight = workbook.CreateCellStyle();
            //设置内容居中
            _styleRight.VerticalAlignment = VerticalAlignment.Center;
            _styleRight.Alignment = HorizontalAlignment.Center;
            //设置字体
            var rightfont = workbook.CreateFont();
            rightfont.FontHeightInPoints = 12;
            _styleRight.SetFont(rightfont);
            //设置边框
            _styleRight.BorderLeft = BorderStyle.Thin;
            _styleRight.BorderRight = BorderStyle.Medium;
            _styleRight.BorderTop = BorderStyle.Thin;
            _styleRight.BorderBottom = BorderStyle.Thin;
            #endregion

            #region 汇总

            _styleTotal = workbook.CreateCellStyle();
            //设置内容居中
            _styleTotal.VerticalAlignment = VerticalAlignment.Center;
            _styleTotal.Alignment = HorizontalAlignment.Center;
            //设置字体
            var totalfont = workbook.CreateFont();
            totalfont.FontHeightInPoints = 12;
            _styleTotal.SetFont(totalfont);
            //设置边框
            _styleTotal.BorderLeft = BorderStyle.Thin;
            _styleTotal.BorderRight = BorderStyle.Thin;
            _styleTotal.BorderTop = BorderStyle.Double;
            _styleTotal.BorderBottom = BorderStyle.Thin;
            #endregion

            #region 10号字
            _style10 = workbook.CreateCellStyle();
            //设置内容居中
            _style10.VerticalAlignment = VerticalAlignment.Center;
            _style10.Alignment = HorizontalAlignment.Center;
            //设置字体
            var font10 = workbook.CreateFont();
            font10.FontHeightInPoints = 10;
            _style10.SetFont(font10);
            //设置边框
            _style10.BorderLeft = BorderStyle.Medium;
            _style10.BorderRight = BorderStyle.Medium;
            _style10.BorderTop = BorderStyle.Medium;
            _style10.BorderBottom = BorderStyle.Medium;
            #endregion

            #region 11号字
            _style11 = workbook.CreateCellStyle();
            //设置字体
            var font11 = workbook.CreateFont();
            font11.FontHeightInPoints = 11;
            _style11.SetFont(font11);
            #endregion

            #region 12号字
            _style12 = workbook.CreateCellStyle();
            //设置字体
            var font12 = workbook.CreateFont();
            font12.FontHeightInPoints = 12;
            _style12.SetFont(font12);
            #endregion

            #region 14号字
            _style14 = workbook.CreateCellStyle();
            //设置字体
            var font14 = workbook.CreateFont();
            font14.FontHeightInPoints = 14;
            font14.Underline = FontUnderlineType.Single;
            _style14.SetFont(font14);
            #endregion

            #region 16号字
            _style16 = workbook.CreateCellStyle();
            //设置内容居中
            _style16.VerticalAlignment = VerticalAlignment.Center;
            _style16.Alignment = HorizontalAlignment.Center;
            //设置字体
            var font16 = workbook.CreateFont();
            font16.FontHeightInPoints = 16;
            font16.IsBold = true;
            font16.Underline = FontUnderlineType.Single;
            _style16.SetFont(font16);
            #endregion

            #region 颜色字体
            _colotStyle = workbook.CreateCellStyle();
            //设置内容居中
            _colotStyle.VerticalAlignment = VerticalAlignment.Center;
            _colotStyle.Alignment = HorizontalAlignment.Center;
            //设置字体
            var colorFont = workbook.CreateFont();
            colorFont.FontHeightInPoints = 12;
            _colotStyle.SetFont(colorFont);
            _colotStyle.FillForegroundColor = HSSFColor.LightBlue.Index;
            _colotStyle.FillPattern = FillPattern.SolidForeground;
            _colotStyle.BorderLeft = BorderStyle.Thin;
            _colotStyle.BorderRight = BorderStyle.Thin;
            _colotStyle.BorderTop = BorderStyle.Thin;
            _colotStyle.BorderBottom = BorderStyle.Thin;
            #endregion
        }
   

        public MemoryStream GetExcelPackage()
        {
            using (var fs = new MemoryStream())
            {
                var workbook = new HSSFWorkbook();


                //var img = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "/gitea-sm.png");

                //var stream = new MemoryStream();//  img.Size
                //img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                //FileStream stream = new FileStream(System.IO.Directory.GetCurrentDirectory() + "/gitea-sm.png", FileMode.Open, FileAccess.Read);
                //var arr = new byte[stream.Length];
                //stream.Read(arr, 0, arr.Length);
                //var arr = File.ReadAllBytes(System.IO.Directory.GetCurrentDirectory() + "/gitea-sm.png");             
               // workbook.AddPicture(arr, PictureType.PNG);

                //stream.Close();

                LoadStyle(workbook);
                var sheet = workbook.CreateSheet(SheetName);
           
                //添加图片
                var data = File.ReadAllBytes(System.IO.Directory.GetCurrentDirectory() + "/gitea-sm.png");
                var pictureIndex = workbook.AddPicture(data, PictureType.PNG);
                var helper = workbook.GetCreationHelper();
                var drawing = sheet.CreateDrawingPatriarch();
                var anchor = helper.CreateClientAnchor();
                anchor.Col1 = 0;//0 index based column
                anchor.Row1 = 0;//0 index based row
               
                var picture = drawing.CreatePicture(anchor, pictureIndex);
                picture.Resize(1,2);





                sheet.DefaultColumnWidth = 4;
                sheet.DisplayGridlines = false;

                SetCell(workbook, sheet, 2, 2, 22, 29, $"発行年月日 {PublishDate.ToString("yyyy年MM月dd日")}", _style12);

                SetCell(workbook, sheet, 5, 11, 1, 13, $"{ToCompany}  御中", _style10);

                SetCell(workbook, sheet, 5, 5, 17, 29, FromCompany, _style12);

                SetCell(workbook, sheet, 6, 6, 17, 29, FromDepartment, _style11);


                ////设置标题             
                SetCell(workbook, sheet, 16, 17, 0, 30, Title, _style16);

                //设置周期               
                SetCell(workbook, sheet, 20, 20, 1, 12, Period.ToString("yyyy年MM月度"), _style14);

                #region  代理商信息
                SetCell(workbook, sheet, 22, 23, 1, 4, "代理店コード", _styleTopLeft);
                SetCell(workbook, sheet, 22, 23, 5, 13, AgentMessage.AgentCode, _styleTopRight);

                SetCell(workbook, sheet, 24, 25, 1, 4, "振込日", _styleLeft);
                SetCell(workbook, sheet, 24, 25, 5, 13, AgentMessage.DemittanceDate?.ToString("yyyy年MM月dd日"), _styleRight);

                SetCell(workbook, sheet, 26, 27, 1, 4, "振込金額", _styleBottomLeft);
                SetCell(workbook, sheet, 26, 27, 5, 13, AgentMessage.DemittanceAmount.ToString(), _styleBottomRight);

              
                #endregion

                #region 代理商帐户信息
                SetCell(workbook, sheet, 22, 23, 17, 20, "金融機関名", _styleTopLeft);
                SetCell(workbook, sheet, 22, 23, 21, 29, AgentAccount.BankName, _styleTopRight);

                SetCell(workbook, sheet, 24, 25, 17, 20, "支店名", _styleLeft);
                SetCell(workbook, sheet, 24, 25, 21, 29, AgentAccount.BranchBankName, _styleRight);

                SetCell(workbook, sheet, 26, 27, 17, 20, "口座番号", _styleLeft);
                SetCell(workbook, sheet, 26, 27, 21, 22, AgentAccount.AccountMode, _dataStyle);
                SetCell(workbook, sheet, 26, 27, 23, 29, AgentAccount.Account, _styleRight);

                SetCell(workbook, sheet, 28, 29, 17, 20, "口座名義", _styleBottomLeft);
                SetCell(workbook, sheet, 28, 29, 21, 29, AgentAccount.AccountOwner, _styleBottomRight);


                #endregion

                #region 列表

                HSSFPalette palette = workbook.GetCustomPalette(); //调色板实例         
                palette.SetColorAtIndex(11, 252, 228, 214);
                _colotStyle.FillForegroundColor = 11;
                SetCell(workbook, sheet, 32, 32, 1, 1, null, _colotStyle);
                SetCell(workbook, sheet, 32, 32, 2, 4, "コード", _colotStyle);
                SetCell(workbook, sheet, 32, 32, 5, 9, "加盟店", _colotStyle);
                SetCell(workbook, sheet, 32, 32, 10, 14, "対象期間", _colotStyle);
                SetCell(workbook, sheet, 32, 32, 15, 18, "決済利用額", _colotStyle);
                SetCell(workbook, sheet, 32, 32, 19, 21, "手数料率", _colotStyle);
                SetCell(workbook, sheet, 32, 32, 22, 25, "手数料額", _colotStyle);
                SetCell(workbook, sheet, 32, 32, 26, 29, "備考", _colotStyle);

                var rowIndex = 33;
                var sn = 1;
                var total = 0m;
                foreach (var item in Data)
                {
                    total += item.RateAmount;
                    SetCell(workbook, sheet, rowIndex, rowIndex, 1, 1, sn++.ToString(), _dataStyle);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 2, 4, item.Code, _dataStyle);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 5, 9, item.Name, _dataStyle);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 10, 14, item.TimeSection, _dataStyle);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 15, 18, item.Amount.ToString(), _dataStyle);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 19, 21, item.Rate.ToString(), _dataStyle);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 22, 25, item.RateAmount.ToString(), _dataStyle);
                    SetCell(workbook, sheet, rowIndex, rowIndex, 26, 29, item.Memo, _dataStyle);
                    rowIndex++;
                }

                SetCell(workbook, sheet, rowIndex, rowIndex, 1, 1, "計", _styleTotal);
                SetCell(workbook, sheet, rowIndex, rowIndex, 2, 4, "", _styleTotal);
                SetCell(workbook, sheet, rowIndex, rowIndex, 5, 9, "", _styleTotal);
                SetCell(workbook, sheet, rowIndex, rowIndex, 10, 14, "税抜き", _styleTotal);
                SetCell(workbook, sheet, rowIndex, rowIndex, 15, 18, "", _styleTotal);
                SetCell(workbook, sheet, rowIndex, rowIndex, 19, 21, "", _styleTotal);
                SetCell(workbook, sheet, rowIndex, rowIndex, 22, 25, total.ToString(), _styleTotal);
                SetCell(workbook, sheet, rowIndex, rowIndex, 26, 29, "", _styleTotal);
                #endregion

                workbook.Write(fs);
                return fs;
            }
        }


        void SetCell(IWorkbook workbook, ISheet sheet, int r, int tor, int c, int toc, dynamic value, ICellStyle style)
        {
            var rang = new CellRangeAddress(r, tor, c, toc);
            sheet.AddMergedRegion(rang);

            var row = sheet.GetRow(r) == null ? sheet.CreateRow(r) : sheet.GetRow(r);
            var cell = row.CreateCell(c);
            if (value != null)
            {
                cell.SetCellValue(value);
            }

            for (int i = rang.FirstRow; i <= rang.LastRow; i++)
            {
                var borderRow = CellUtil.GetRow(i, sheet);
                for (int j = rang.FirstColumn; j <= rang.LastColumn; j++)
                {
                    var singleCell = CellUtil.GetCell(borderRow, (short)j);
                    singleCell.CellStyle = style;
                }
            }
         
            cell.CellStyle = style;
        }

    }

}
