using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Linq;

namespace BlazorClientDemo02
{
    public class ArticleService
    {
        readonly string _connectionString;
        public ArticleService()
        {
            _connectionString = string.Format("Data Source={0}/data.db", System.IO.Directory.GetCurrentDirectory());
        }
        SqliteConnection CreateConnection()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }
        public async Task<IEnumerable<ArticleModel>> GetArticlesAsync()
        {
            
            using (var con = CreateConnection())
            {
                return await con.QueryAsync<ArticleModel>("select * from articles");
            }
        }

        public async Task<ArticleModel> GetArticleAsync(int id)
        {
            using (var con = CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<ArticleModel>("select * from articles where id=@id", new { id });
            }
        }
        public async Task<int> EditAsync(ArticleModel article)
        {
            using (var con = CreateConnection())
            {
                return await con.ExecuteAsync("update articles set title=@Title,content=@Content where id=@ID", article);
            }
        }
    }
}
