using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo01.Services
{
    public class MsSqlReadDapper : ReadDapper
    {
        public MsSqlReadDapper(IEnumerable<IDbConnection> connections, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<Dictionary<string, string>>();
            var readConnectionStrings = connectionStrings.Where(s => s.Key.ToLower().Contains("read"));
            _connection = connections.FirstOrDefault(c => c.GetType().Name == "SqlConnection");
            _connection.ConnectionString = connectionStrings.Where(s => s.Key.ToLower().Contains("mssql")).FirstOrDefault().Value;
        }
    }
}
