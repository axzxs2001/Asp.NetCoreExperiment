using Dapper;
using DapperPlus;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuartzNetDemo4.Model
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

        }
    }
}
