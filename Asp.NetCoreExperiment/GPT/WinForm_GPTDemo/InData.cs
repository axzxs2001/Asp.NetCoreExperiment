using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.DirectoryServices.ActiveDirectory;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_GPTDemo
{
    //trns_type 交易类型 String 2 Y 1、身份识别2、修改密码
    //read_way 读卡类型 String 2 Y 1、社保卡；2、医保电子凭证；3、身份证号码（身份证号码仅供测试使用，正式上线后将取消）；4、终端扫描医保电子凭证；5、终端刷脸
    //medins_no 定点医药机构编号 String 12 Y
    //fixmedins_name    定点医药机构名称 String 20 Y
    //businessType 业务类型 String 5 Y 见表 C.1（业务类型为结算时，会拉起密码核验框，如：01301）
    //opter 经办人编码 String 30 Y 按地方要求传入经办人/终端编号
    //opter_name 经办人姓名 String 50 Y 按地方要求传入经办人姓名终端名称
    //opter_type 经办人类别 String 3 Y 1-经办人；2-自助终端；3-移动终端
    //dept_no 科室编码 String 20 Y
    //dept_name 科室名称 String 30 Y
    //mdtrtarea_admvs    就医地行政区划代码    String 6 Y
    //dev_no 设备编号 String 100
    //dev_safe_info 设备安全信息 String 2000
    //cainfo 数字签名信息 String 1024
    //signtype 签名类型 String 10
    //infver 接口版本号 String 6 Y 例如：“V1.0”，版本号由医保下发通知。
    //inf_time 交易时间 Date 19 Y
    //sign_no 交易签到流水号 String 30 通过签到【9001】交易获取
    //begntime 开始时间 Date 30 获取历史参保信息时传入
    //------------------
    //把上面数据生成C#实体类，并保留原来的名称，加上注释
    public class Request
    {
        /// <summary>
        /// 交易码
        /// </summary>
        public string trns_cd { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public string trns_type { get; set; }
        /// <summary>
        /// 读卡类型
        /// </summary>
        public string read_way { get; set; }
        /// <summary>
        /// 定点医药机构编号
        /// </summary>
        public string medins_no { get; set; }
        /// <summary>
        /// 定点医药机构名称
        /// </summary>
        public string fixmedins_name { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string businessType { get; set; }
        /// <summary>
        /// 经办人编码
        /// </summary>
        public string opter { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string opter_name { get; set; }
        /// <summary>
        /// 经办人类别
        /// </summary>
        public string opter_type { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string dept_no { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string dept_name { get; set; }
        /// <summary>
        /// 就医地行政区划代码
        /// </summary>
        public string mdtrtarea_admvs { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string dev_no { get; set; }
        /// <summary>
        /// 设备安全信息
        /// </summary>
        public string dev_safe_info { get; set; }
        /// <summary>
        /// 数字签名信息
        /// </summary>
        public string cainfo { get; set; }
        /// <summary>
        /// 签名类型
        /// </summary>
        public string signtype { get; set; }
        /// <summary>
        /// 接口版本号
        /// </summary>
        public string infver { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime inf_time { get; set; }
        /// <summary>
        /// 交易签到流水号
        /// </summary>
        public string sign_no { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime begntime { get; set; }
    }
}




