using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinForm_GPTDemo
{

    //1 psn_no 人员编号 字符型 30 Y
    //2 psn_cert_type 人员证件类型 字符型 6 Y Y
    //3 certno 证件号码 字符型 50 Y
    //4 psn_name 人员姓名 字符型 50 Y
    //5 gend 性别 字符型 6 Y Y
    //6 naty 民族 字符型 3 Y
    //7 brdy 出生日期 日期型yyyy-MM-dd
    //8 age 年龄 数值型 4,1 Y
    //9 expContent 字段扩展 字符型 4000
    //把上面数据生成C#实体类，名称是BaseInfo，并保留原来的名称，加上注释
    /// <summary>
    /// BaseInfo
    /// </summary>
    public class BaseInfo
    {
        /// <summary>
        /// 人员编号
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 人员证件类型
        /// </summary>
        public string psn_cert_type { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string certno { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string psn_name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string gend { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string naty { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime brdy { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public decimal age { get; set; }

        /// <summary>
        /// 字段扩展
        /// </summary>
        public string expContent { get; set; }
    }

}
