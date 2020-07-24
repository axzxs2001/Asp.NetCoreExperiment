using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SqlConnectionCountTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            for (var i = 0; i < 1000000; i++)
            {
                new Thread(Client).Start();
                // F();
            }
        }
        static void Client()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://localhost:5000/test").Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("返回值：" + content);
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("返回值：" + content);
                }
            }
        }
        static void F()
        {
            try
            {
                using (var conn = new SqlConnection("Data Source=.;Initial Catalog=StarPay;Persist Security Info=True;User ID=sa;Password=sa;Max Pool Size=500"))
                {
                    conn.Open();
                    var sql = "select 1;WAITFOR DELAY '00:00:03';";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

    }
}
