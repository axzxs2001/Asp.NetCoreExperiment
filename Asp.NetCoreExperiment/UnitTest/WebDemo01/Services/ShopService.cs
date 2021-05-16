using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo01.Services
{
    public interface IShopService
    {
        List<Goods> GetAllGoods();
    }
    public class ShopService : IShopService
    {
        private readonly IDapperPlusWrite _mySqlDapperWrite;
        private readonly IDapperPlusWrite _msSqlDapperWrite;
        private readonly IDapperPlusRead _mySqlDapperRead;
        private readonly IDapperPlusRead _msSqlDapperRead;
        public ShopService(IEnumerable<IDapperPlusWrite> dapperWrites, IEnumerable<IDapperPlusRead> dapperReads)
        {
            foreach (var dapperWrite in dapperWrites)
            {
                switch (dapperWrite)
                {
                    case MySqlDapperPlusWrite mySqlDapperPlusWrite:
                        _mySqlDapperWrite = mySqlDapperPlusWrite;
                        break;
                    case MsSqlDapperPlusWrite msSqlDapperPlusWrite:
                        _msSqlDapperWrite = msSqlDapperPlusWrite;
                        break;
                }

            }
            foreach (var dapperRead in dapperReads)
            {
                switch (dapperRead)
                {
                    case MySqlDapperPlusRead mySqlDapperPlusRead:
                        _mySqlDapperRead = mySqlDapperPlusRead;
                        break;
                    case MsSqlDapperPlusRead msSqlDapperPlusRead:
                        _msSqlDapperRead = msSqlDapperPlusRead;
                        break;
                }

            }
        }

        public List<Goods> GetAllGoods()
        {
            using var con = new SqlConnection();
            var sql = "select * from Goodses";
            var list = con.Query<Goods>(sql, new { id = 1 }).ToList();
            return list;
        }
    }

    public class Goods
    {

    }
}
