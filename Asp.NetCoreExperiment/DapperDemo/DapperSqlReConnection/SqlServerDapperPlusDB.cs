using Dapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DapperSqlReConnection
{
    /// <summary>
    /// IDapperPlusDB数据库类型 
    /// </summary>
    public class SqlServerDapperPlusDB : DapperPlusDB, ISqlServerDapperPlusDB
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dbConnection">连接对象</param>
        /// <param name="connectionString">连接字符串</param>
        public SqlServerDapperPlusDB(SqlConnection dbConnection, IOptions<ConnectionString> connectionStringOpt) : base(dbConnection, connectionStringOpt.Value.SqlServerConnectionString)
        {
            DateTime d = DateTime.Now;
            try
            {
                //没有镜像时不会起作用Failover Partner
                new SqlConnection(connectionStringOpt.Value.SqlServerConnectionString+ "Failover Partner=127.0.0.1;").Open();
            }
            catch(Exception exc)
            {
                //连接字符串自动切换
                Console.WriteLine(exc.Message);
                var spn= DateTime.Now-d;
                Console.WriteLine(spn.TotalSeconds);
                base.GetConnection().ConnectionString = connectionStringOpt.Value.SqlServerConnectionStringStandby;
                var middleConnectionString = connectionStringOpt.Value.SqlServerConnectionString;
                connectionStringOpt.Value.SqlServerConnectionString = connectionStringOpt.Value.SqlServerConnectionStringStandby;
                connectionStringOpt.Value.SqlServerConnectionStringStandby = middleConnectionString;
            }
        }
    }
}
