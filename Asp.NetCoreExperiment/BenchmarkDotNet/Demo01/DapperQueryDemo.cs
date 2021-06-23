using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01
{
    public class DapperQueryDemo : IDemo
    {
        public void Run()
        {
            BenchmarkRunner.Run<DapperTest>();
        }
    }
    [MemoryDiagnoser]
    public class DapperTest
    {
        static int count = 5;
        #region 单次
        [Benchmark]
        public List<Product> GetProduct()
        {
            using var con = new SqlConnection("server=.;database=AdventureWorks2016;uid=sa;pwd=sa;");
            return con.Query<Product>("select * from production.product").ToList();
        }

        [Benchmark]
        public async Task<List<Product>> GetProductAsync()
        {
            using var con = new SqlConnection("server=.;database=AdventureWorks2016;uid=sa;pwd=sa;");
            return (await con.QueryAsync<Product>("select * from production.product")).ToList();
        }
        #endregion

        #region 多次
        [Benchmark]
        public async Task<List<Product>> GetProductAsync00()
        {
            var list = new List<Product>();
            for (var i = 0; i < count; i++)
            {
                using var con = new SqlConnection("server=.;database=AdventureWorks2016;uid=sa;pwd=sa;");
                list.AddRange(await con.QueryAsync<Product>("select * from production.product"));
            }
            return list;
        }

        [Benchmark]
        public async Task<List<Product>> GetProductAsync01()
        {
            var list = new List<Product>();
            var tasks = new List<Task>();
            for (var i = 0; i < count; i++)
            {
                var result = GetProAsync();
                tasks.Add(result);
            }
            await Task.WhenAll(tasks);
            list.AddRange(tasks.SelectMany(s => ((Task<IEnumerable<Product>>)s).Result));
            return list;
            async Task<IEnumerable<Product>> GetProAsync()
            {
                using var con = new SqlConnection("server=.;database=AdventureWorks2016;uid=sa;pwd=sa;");
                return await con.QueryAsync<Product>("select * from production.product");
            }
        }
        #endregion
    }


    public class Product
    {
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string MakeFlag { get; set; }
        public string FinishedGoodsFlag { get; set; }
        public string Color { get; set; }
        public short SafetyStockLevel { get; set; }
        public short ReorderPoint { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        public string SizeUnitMeasureCode { get; set; }
        public string WeightUnitMeasureCode { get; set; }
        public float Weight { get; set; }
        public int DaysToManufacture { get; set; }
        public string ProductLine { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }
        public int ProductSubcategoryID { get; set; }
        public int ProductModelID { get; set; }
        public DateTime SellStartDate { get; set; }
        public DateTime SellEndDate { get; set; }
        public DateTime DiscontinuedDate { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
