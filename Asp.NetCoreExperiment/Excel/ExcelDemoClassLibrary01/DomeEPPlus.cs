using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ExcelDemoClassLibrary01
{
    public class DomeEPPlus
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

        public ExcelPackage GetExcelPackage()
        {
            //表格边框色
            var baseColor = Color.Black;
            var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add(SheetName);
            worksheet.View.ShowGridLines = false;
            worksheet.DefaultColWidth = 5;
            //添加图片
            var img = worksheet.Drawings.AddPicture("gitea", Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "/gitea-sm.png"));
            img.SetPosition(80, 500);
            img.SetSize(60, 60);

            //R3 C23-30
            worksheet.Cells[3, 23, 3, 30].Merge = true;//合并单元格
            worksheet.Cells[3, 23, 3, 30].Style.Font.Size = 12;
            worksheet.Cells[3, 23, 3, 30].Value = PublishDate.ToString("発行年月日 yyyy年MM月dd日");

            //R6 C18
            worksheet.Cells[6, 18].Value = FromCompany;
            worksheet.Cells[6, 18].Style.Font.Size = 12;

            //R7 C18
            worksheet.Cells[7, 18].Value = FromDepartment;
            worksheet.Cells[7, 18].Style.Font.Size = 11;

            //R6-12 C2-14
            worksheet.Cells[6, 2, 12, 14].Style.Border.BorderAround(ExcelBorderStyle.Medium, baseColor);
            //R2 C2-14
            worksheet.Cells[9, 2, 9, 14].Merge = true;//合并单元格
            worksheet.Cells[9, 2, 9, 14].Value = ToCompany + "  御中";
            worksheet.Cells[9, 2, 9, 14].Style.Font.Size = 12;
            worksheet.Cells[9, 2, 9, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
            worksheet.Cells[9, 2, 9, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//垂直居中
            //R17-18 C1-31
            worksheet.Cells[17, 1, 18, 31].Merge = true;//合并单元格
            worksheet.Cells[17, 1, 18, 31].Value = Title;
            worksheet.Cells[17, 1, 18, 31].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
            worksheet.Cells[17, 1, 18, 31].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//垂直居中
            worksheet.Cells[17, 1, 18, 31].Style.Font.Size = 16;
            worksheet.Cells[17, 1, 18, 31].Style.Font.Bold = true;
            worksheet.Cells[17, 1, 18, 31].Style.Font.UnderLine = true;
            worksheet.Cells[17, 1, 18, 31].Style.Font.UnderLineType = ExcelUnderLineType.Single;
            //R21 C2-13
            worksheet.Cells[21, 2, 21, 13].Merge = true;//合并单元格
            worksheet.Cells[21, 2, 21, 13].Value = Period.ToString("yyyy年MM月度");
            worksheet.Cells[21, 2, 21, 13].Style.Font.Size = 14;
            worksheet.Cells[21, 2, 21, 13].Style.Font.UnderLine = true;
            worksheet.Cells[21, 2, 21, 13].Style.Font.UnderLineType = ExcelUnderLineType.Single;

            #region 代理商信息 R23-28 C2-14          
            //设置代理商信息
            void SetAgentMessageCell(int r, int c, int tor, int toc, dynamic value)
            {
                worksheet.Cells[r, c, tor, toc].Merge = true;//合并单元格
                worksheet.Cells[r, c, tor, toc].Value = value;
                worksheet.Cells[r, c, tor, toc].Style.Font.Size = 12;
                worksheet.Cells[r, c, tor, toc].Style.Border.BorderAround(ExcelBorderStyle.Thin, baseColor);
                worksheet.Cells[r, c, tor, toc].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
                worksheet.Cells[r, c, tor, toc].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//垂直居中
            }
            //R23-24 C2-5
            SetAgentMessageCell(23, 2, 24, 5, "代理店コード");
            //R23-24 C6-14
            SetAgentMessageCell(23, 6, 24, 14, AgentMessage.AgentCode);
            //R25-26 C2-5 
            SetAgentMessageCell(25, 2, 26, 5, "振込日");
            //R25-26 C6-14      
            SetAgentMessageCell(25, 6, 26, 14, AgentMessage.DemittanceDate);
            //R27-28 C2-5      
            SetAgentMessageCell(27, 2, 28, 5, "振込金額");
            //R27-28 C6-14      
            SetAgentMessageCell(27, 6, 28, 14, AgentMessage.DemittanceAmount);

            //R23-28 C2-14
            worksheet.Cells[23, 2, 28, 14].Style.Border.BorderAround(ExcelBorderStyle.Medium, baseColor);
            #endregion

            #region 代理产帐户 R23-30 C18-30           
            //设置帐户内容
            void SetAgentAccountCell(int r, int c, int tor, int toc, dynamic value)
            {
                worksheet.Cells[r, c, tor, toc].Merge = true;//合并单元格
                worksheet.Cells[r, c, tor, toc].Value = value;
                worksheet.Cells[r, c, tor, toc].Style.Font.Size = 12;
                worksheet.Cells[r, c, tor, toc].Style.Border.BorderAround(ExcelBorderStyle.Thin, baseColor);
                worksheet.Cells[r, c, tor, toc].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
                worksheet.Cells[r, c, tor, toc].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//垂直居中
            }
            //R23-24 C18-21
            SetAgentAccountCell(23, 18, 24, 21, "金融機関名");
            //R23-24 C22-30
            SetAgentAccountCell(23, 22, 24, 30, AgentAccount.BankName);
            //R25-26 C18-21
            SetAgentAccountCell(25, 18, 26, 21, "支店名");
            //R25-26 C22-30
            SetAgentAccountCell(25, 22, 26, 30, AgentAccount.BranchBankName);
            //R27-28 C18-21
            SetAgentAccountCell(27, 18, 28, 21, "口座番号");
            //R27-28 C22-23
            SetAgentAccountCell(27, 22, 28, 23, AgentAccount.AccountMode);
            //R27-28 C24-30
            SetAgentAccountCell(27, 24, 28, 30, AgentAccount.Account);
            //R29-30 C18-21
            SetAgentAccountCell(29, 18, 30, 21, "口座名義");
            //R29-30 C22-30
            SetAgentAccountCell(29, 22, 30, 30, AgentAccount.AccountOwner);

            //R23-30 C18-30
            worksheet.Cells[23, 18, 30, 30].Style.Border.BorderAround(ExcelBorderStyle.Medium, baseColor);
            #endregion

            #region 数据表格填充 
            int dataRowHeight = 25;
            #region 设置列表标题          
            void SetTitle(int r, int c, int tor, int toc, string value)
            {
                worksheet.Row(r).Height = dataRowHeight;
                worksheet.Cells[r, c, tor, toc].Merge = true;//合并单元格
                worksheet.Cells[r, c, tor, toc].Value = value;
                worksheet.Cells[r, c, tor, toc].Style.Font.Size = 12;
                worksheet.Cells[r, c, tor, toc].Style.Border.BorderAround(ExcelBorderStyle.Thin, baseColor);
                worksheet.Cells[r, c, tor, toc].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
                worksheet.Cells[r, c, tor, toc].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//垂直居中
                worksheet.Cells[r, c, tor, toc].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[r, c, tor, toc].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(252, 228, 214));
            }
            //R33 C2-2
            SetTitle(33, 2, 33, 2, "");
            //R33 C3-5
            SetTitle(33, 3, 33, 5, "コード");
            //R33 C6-10
            SetTitle(33, 6, 33, 10, "加盟店");
            //R33 C11-15
            SetTitle(33, 11, 33, 15, "対象期間");
            //R33 C16-19  
            SetTitle(33, 16, 33, 19, "決済利用額");
            //R33 C20-22  
            SetTitle(33, 20, 33, 22, "手数料率");
            //R33 C23-26  
            SetTitle(33, 23, 33, 26, "手数料額");
            //R33 C27-30  
            SetTitle(33, 27, 33, 30, "備考");
            #endregion
            #region 设置列表内容
            void SetContent(int r, int c, int tor, int toc, dynamic value)
            {
                worksheet.Row(r).Height = dataRowHeight;
                worksheet.Cells[r, c, tor, toc].Merge = true;//合并单元格
                worksheet.Cells[r, c, tor, toc].Value = value;
                worksheet.Cells[r, c, tor, toc].Style.Font.Size = 12;
                worksheet.Cells[r, c, tor, toc].Style.Border.BorderAround(ExcelBorderStyle.Thin, baseColor);
                worksheet.Cells[r, c, tor, toc].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
                worksheet.Cells[r, c, tor, toc].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//垂直居中
            }
            int beginRow = 34;
            decimal total = 0;
            int sn = 1;
            foreach (var item in Data)
            {
                total += item.RateAmount;
                //C2-2
                SetContent(beginRow, 2, beginRow, 2, sn++);
                //C3-5
                SetContent(beginRow, 3, beginRow, 5, item.Code);
                //C6-10
                SetContent(beginRow, 6, beginRow, 10, item.Name);
                //C11-15
                SetContent(beginRow, 11, beginRow, 15, item.TimeSection);
                //C16-19  
                SetContent(beginRow, 16, beginRow, 19, item.Amount);
                //C20-22  
                SetContent(beginRow, 20, beginRow, 22, item.Rate);
                //C23-26  
                SetContent(beginRow, 23, beginRow, 26, item.RateAmount);
                //C27-30  
                SetContent(beginRow, 27, beginRow, 30, item.Memo);

                beginRow++;
            }
            #endregion
            #region 设置汇总行
            void SetTotal(int r, int c, int tor, int toc, dynamic value)
            {
                worksheet.Row(r).Height = dataRowHeight;
                worksheet.Cells[r, c, tor, toc].Merge = true;//合并单元格
                worksheet.Cells[r, c, tor, toc].Value = value;
                worksheet.Cells[r, c, tor, toc].Style.Font.Size = 12;
                worksheet.Cells[r, c, tor, toc].Style.Border.BorderAround(ExcelBorderStyle.Thin, baseColor);
                worksheet.Cells[r, c, tor, toc].Style.Border.Top.Style = ExcelBorderStyle.Double;
                worksheet.Cells[r, c, tor, toc].Style.Border.Top.Color.SetColor(baseColor);
                worksheet.Cells[r, c, tor, toc].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
                worksheet.Cells[r, c, tor, toc].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//垂直居中
            }
            //C2-2
            SetTotal(beginRow, 2, beginRow, 2, "計");
            //C3-5
            SetTotal(beginRow, 3, beginRow, 5, "");
            //C6-10
            SetTotal(beginRow, 6, beginRow, 10, "");
            //C11-15
            SetTotal(beginRow, 11, beginRow, 15, "税抜き");
            //C16-19  
            SetTotal(beginRow, 16, beginRow, 19, "");
            //C20-22  
            SetTotal(beginRow, 20, beginRow, 22, "");
            //C23-26  
            SetTotal(beginRow, 23, beginRow, 26, total);
            //C27-30  
            SetTotal(beginRow, 27, beginRow, 30, "");
            #endregion
            #endregion



            return package;
        }

    }
    public class AgentMessage
    {
        public string AgentCode { get; set; }

        public DateTime? DemittanceDate { set; get; }

        public decimal? DemittanceAmount { get; set; }
    }
    public class AgentAccount
    {
        public string BankName { get; set; }

        public string BranchBankName { get; set; }

        public string Account { get; set; }

        public string AccountMode { get; set; }

        public string AccountOwner { get; set; }


    }
    public class Item
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public string TimeSection { get; set; }

        public decimal Amount { get; set; }

        public double Rate { get; set; }

        public decimal RateAmount { get; set; }
        public string Memo { get; set; }
    }
}
