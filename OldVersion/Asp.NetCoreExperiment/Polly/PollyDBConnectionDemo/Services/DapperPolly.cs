using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using System.Data;
using Polly;
using Microsoft.Extensions.Logging;

namespace PollyDBConnectionDemo.Services
{
    public class DapperPolly
    {
        public int GetCount()
        {
            using (var con = new NpgsqlConnection("Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=pp;Pooling=true;MinPoolSize=1;MaxPoolSize=100;CommandTimeout=0;"))
            {
                var list = con.Query<dynamic>("select * from a");
                return list.Count();
            }
        }
    }


 
}
