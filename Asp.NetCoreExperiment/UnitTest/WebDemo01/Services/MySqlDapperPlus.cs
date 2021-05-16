using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WebDemo01.Services
{
    public class MySqlDapperPlus : DapperPlus
    {
        public MySqlDapperPlus(IEnumerable<IDbConnection> connections, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<Dictionary<string, string>>();          
            _connection = connections.FirstOrDefault(c => c.GetType().Name == "MySqlConnection");
            _connection.ConnectionString = connectionStrings.Where(s => s.Key.ToLower().Contains("mysql")).FirstOrDefault().Value;
        }
    }
}
