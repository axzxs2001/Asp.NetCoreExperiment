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
        private readonly IDapperPlusWrite _writeDapper;
        private readonly IEnumerable<IDapperPlusRead> _readDappers;
        public ShopService(IDapperPlusWrite writeDapper, IEnumerable<IDapperPlusRead> readDappers)
        {
            _writeDapper = writeDapper;
            _readDappers = readDappers;

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
