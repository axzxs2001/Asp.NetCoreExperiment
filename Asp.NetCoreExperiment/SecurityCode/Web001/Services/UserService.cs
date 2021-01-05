using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web001.Models;

namespace Web001.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly string _connectionString;
        public UserService(string connectionString, ILogger<UserService> logger)
        {
            _connectionString = connectionString;
            _logger = logger;

        }
        /// <summary>
        /// SQL注入
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<UserModel>> GetUsersAsync(string name)
        {
            _logger.LogInformation("SQL注入");

            using var db = new MySqlConnection(_connectionString);
            var sql = $"select * from persons where name like '%{name}%'";
            var cmd = new MySqlCommand(sql, db);
            return (await db.QueryAsync<UserModel>(sql)).ToList();
        }
    }
}
