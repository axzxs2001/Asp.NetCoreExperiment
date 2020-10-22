using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrometheusSample.Services
{
    public class OrderService : IOrderService
    {
        private readonly string _connectionString;
        public OrderService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }

        public async Task<bool> Register(string username)
        {
            using (var con = new MySqlConnection(_connectionString))
            {
                var sql = "select count(*) as sl from users where username=@username";
                var count = await con.ExecuteScalarAsync<int>(sql, new { username });
                if (count == 0)
                {
                    sql = "insert into users(username) values(@username)";
                    await con.ExecuteAsync(sql, new { username });
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
