using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WebDemo01.Services
{
    public class MsSqlDapperPlus : DapperPlus
    {
        public MsSqlDapperPlus(IEnumerable<IDbConnection> connections, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<Dictionary<string, string>>();          
            _connection = connections.FirstOrDefault(c => c.GetType().Name == "SqlConnection");
            _connection.ConnectionString = connectionStrings.Where(s => s.Key.ToLower().Contains("mssql")).FirstOrDefault().Value;
        }
    }
}
