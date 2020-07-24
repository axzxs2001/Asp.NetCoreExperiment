using Dapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DapperSqlReConnection
{
    /// <summary>
    /// IDapperPlusDB数据库类型 
    /// </summary>
    public class PostgreSqlDapperPlusDB : DapperPlusDB, IPostgreSqlDapperPlusDB
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dbConnection">连接对象</param>
        /// <param name="connectionString">连接字符串</param>
        public PostgreSqlDapperPlusDB(NpgsqlConnection dbConnection, IOptions<ConnectionString> connectionStringOpt) : base(dbConnection, connectionStringOpt.Value.PostgreSqlConnectionString)
        {

        }
    }
}
