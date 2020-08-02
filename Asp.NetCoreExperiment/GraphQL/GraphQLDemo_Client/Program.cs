using Microsoft.VisualBasic;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace GraphQLDemo_Client
{
    class Program
    {
        async static Task Main(string[] args)
        {
            await GetAll();
            await GetByID(1);
        }

        async static Task<string> HttpGetAsync(string query)
        {
            var url = $"http://localhost:5000/test?query={query}";
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        async static Task GetAll()
        {
            //全部查询 字段 Fields
            var query = "{persons{id name }}";
            var content = await HttpGetAsync(query);          
            Console.WriteLine(content);
        }


        async static Task GetByID(int id)
        {
            //条件查询 参数 Arguments
            var query = $"{{person(id:{id}){{id name }} childs(pid:{id}){{id name}}}}";
            var content = await HttpGetAsync(query);           
            Console.WriteLine(content);
        }
    }
}
