using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace WebSiloHost.Repository
{
    public class SettlementRepository : ISettlementRepository
    {
        readonly IConfiguration _configuration;
        readonly SqlConnection _connection;
        public SettlementRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("DefaultConnectionString");
            _connection = new SqlConnection(connectionString);
        }

        public Task<bool> Settlement(DateTime dateTime)
        {


            return Task.FromResult(true);
        }
    }
}
