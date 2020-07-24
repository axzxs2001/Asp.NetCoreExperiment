using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model.生成最大编号
{
    /// <summary>
    /// 生成最大编号类
    /// </summary>
    public class CreateSN : ICreateSN
    {
        string _conString;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">连接字符串类</param>
        public CreateSN(IOptions<ConnectionSetting> options)
        {
            _conString = options.Value.DefaultConnection;
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public string GetSN(string typeName)
        {
            using (var con = new SqlConnection(_conString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "getsn";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;  
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@typename", Value = typeName });
                var par = new SqlParameter();
                par.ParameterName = "@sn";
                par.Direction = System.Data.ParameterDirection.Output;
                par.SqlDbType = System.Data.SqlDbType.VarChar;
                par.Size = 30;
                cmd.Parameters.Add(par);
                con.Open();
                cmd.ExecuteReader();
                return par.Value.ToString();
            }
        }
    }
}
