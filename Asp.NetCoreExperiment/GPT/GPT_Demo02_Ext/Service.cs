using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace GPT_Demo02_Ext
{
    public interface IRenYuanService
    {
        Task<bool> Add(RenYuan renYuan);
    }
    /// <summary>
    /// 商品Service
    /// </summary>
    public class RenYuanService : IRenYuanService
    {
        private readonly IDbConnection _conn;
        public RenYuanService(IDbConnection conn)
        {
            _conn = conn;
        }
        /// <summary>
        /// 添加人员
        /// </summary>
        /// <param name="renYuan"></param>
        /// <returns></returns>
        public async Task<bool> Add(RenYuan renYuan)
        {
            var sql = "insert into RenYuan(MingCheng,XingBie,ShengRi) values(@MingCheng,@XingBie,@ShengRi)";
            var result = await _conn.ExecuteAsync(sql, renYuan);
            return result > 0;
        }
    }
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
    //参照上面的接口和类生成ZuHu的Service接口和类，别外添加一个逻辑删除的方法，把ZhuangTai改为0
    public interface IZuHuService
    {
        Task<bool> Add(ZuHu zuHu);
        Task<bool> Delete(int bianHao);
    }
    public class ZuHuService : IZuHuService
    {
        private readonly IDbConnection _conn;
        public ZuHuService(IDbConnection conn)
        {
            _conn = conn;
        }
        /// <summary>
        /// 添加组合
        /// </summary>
        /// <param name="zuHu"></param>
        /// <returns></returns>
        public async Task<bool> Add(ZuHu zuHu)
        {
            var sql = "insert into ZuHu(MingCheng,LeiXing,ZhuangTai,ChuangJianShiJian) values(@MingCheng,@LeiXing,@ZhuangTai,@ChuangJianShiJian)";
            var result = await _conn.ExecuteAsync(sql, zuHu);
            return result > 0;
        }
        /// <summary>
        /// 逻辑删除组合
        /// </summary>
        /// <param name="bianHao"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int bianHao)
        {
            var sql = "update ZuHu set ZhuangTai=0 where BianHao=@BianHao";
            var result = await _conn.ExecuteAsync(sql, new { BianHao = bianHao });
            return result > 0;
        }
    }
}