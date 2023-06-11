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
 
   
}