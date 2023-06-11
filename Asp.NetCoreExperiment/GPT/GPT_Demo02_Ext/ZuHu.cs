using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT_Demo02_Ext
{
    //参照上面类把下面数据生成类
    //租户：编号，名称，类型，状态，创建时间
    public class ZuHu
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int BianHao { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string MingCheng { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int LeiXing { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int ZhuangTai { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [YBDateTime("yyyyMMdd")]
        public DateTime ChuangJianShiJian { get; set; }
        public override string ToString()
        {
            return $"编号：{BianHao}，名称：{MingCheng}，类型：{LeiXing}，状态：{ZhuangTai}，创建时间：{ChuangJianShiJian}";
        }
    }
}
