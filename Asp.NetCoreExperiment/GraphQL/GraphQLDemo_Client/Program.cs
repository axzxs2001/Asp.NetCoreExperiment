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
            while (true)
            {
                Console.WriteLine("1、字段  2、参数  3、别名  4、片段  5、变量片段  6、操作名  7、变量");
                var key = Console.ReadLine();
                switch (key)
                {
                    case "1":
                        await GetAll();
                        break;
                    case "2":
                        await GetByID(1);
                        break;
                    case "3":
                        await GetAllAliases();
                        break;
                    case "4":
                        await GetByIDFragments(1);
                        break;
                    case "5":
                        await GetByIDFragmentsVar();
                        break;
                    case "6":
                        await GetItems();
                        break;
                    case "7":
                        await GetByIDVariables(2);
                        break;

                }
            }
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
        /// <summary>
        /// Fields
        /// </summary>
        /// <returns></returns>
        async static Task GetAll()
        {
            //全部查询 字段 Fields
            var query = "{items{id name }}";
            var content = await HttpGetAsync(query);
            Console.WriteLine(content);
        }

        /// <summary>
        /// Arguments
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        async static Task GetByID(int id)
        {
            //条件查询 参数 Arguments
            var query = $"{{item(id:{id}){{id name }}}}";
            var content = await HttpGetAsync(query);
            Console.WriteLine(content);
        }
        /// <summary>
        /// Aliases
        /// </summary>
        /// <returns></returns>

        async static Task GetAllAliases()
        {
            //全部查询 字段 Fields
            var query = "{is:items{id name }}";
            var content = await HttpGetAsync(query);
            Console.WriteLine(content);
        }
        /// <summary>
        /// Fragments
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        async static Task GetByIDFragments(int id)
        {
            var query = $"{{item(id:{id}){{id name }} childs(pid:{id}){{id name}}}}";
            var content = await HttpGetAsync(query);
            Console.WriteLine(content);
        }
        /// <summary>
        /// Fragments Var
        /// </summary>
        /// <returns></returns>
        async static Task GetByIDFragmentsVar()
        {
            var query = $"{{yes:queryistrue(istrue:true){{id name istrue }} error:queryistrue(istrue:false){{id name istrue }}}}";
            var content = await HttpGetAsync(query);
            Console.WriteLine(content);
        }
        /// <summary>
        /// Operation name
        /// </summary>
        /// <returns></returns>
        async static Task GetItems()
        {
            //全部查询 字段 Fields
            var query = "query GetItems{entity{id name}}";
            var content = await HttpGetAsync(query);
            Console.WriteLine(content);
        }
        /// <summary>
        /// Variables
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        async static Task GetByIDVariables(int id)
        {
            var query = $"{{item(id:int){{id name}} }}";
            var content = await HttpGetAsync(query);
            Console.WriteLine(content);
        }
    }
}
