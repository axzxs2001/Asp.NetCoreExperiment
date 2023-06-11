using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT_Demo02_Ext
{ 
    //参照上面的接口和类生成ZuHu的Service接口和类，另外添加一个逻辑删除的方法，把ZhuangTai改为0
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
