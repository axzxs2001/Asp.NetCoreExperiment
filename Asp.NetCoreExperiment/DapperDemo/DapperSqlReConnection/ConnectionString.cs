using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSqlReConnection
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    public class ConnectionString
    {
        //sql 连接字符串
        public string SqlServerConnectionString { get; set; }
        //备用sql连接字符串
        public string SqlServerConnectionStringStandby { get; set; }
        //pg 连接字符串
        public string PostgreSqlConnectionString { get; set; }
    }
}
