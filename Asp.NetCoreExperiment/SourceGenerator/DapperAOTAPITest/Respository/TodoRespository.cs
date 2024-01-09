using Dapper;
using Microsoft.Data.SqlClient;

namespace DapperAOTAPITest.Respository
{
    public class TodoRespository : ITodoRespository
    {
        public IEnumerable<T> Query<T>()
        {
            using (var conn = new SqlConnection("Data Source=.;Initial Catalog=Test;Integrated Security=True"))
            {
                return conn.Query<T>("select * from Todo ");
            }
        }
    }
}
