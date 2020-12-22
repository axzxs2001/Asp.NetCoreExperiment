using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web001.Models;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;
using Dapper;
using System.ComponentModel.Design;

namespace Web001.Services
{
    public class UserService: IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly string _connectionString;
        public UserService(string connectionString, ILogger<UserService> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }
        public async Task<List<UserModel>> GetUsersAsync(string name)
        {
            using (var db = new MySqlConnection(_connectionString))
            {
                var sql = $"select * from users where name like %{name}%";
                return (await db.QueryAsync<UserModel>(sql)).ToList();
            }
        }
    }
}
