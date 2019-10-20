using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace HttpClientDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:4800";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);

            var request = new HttpRequestMessage(HttpMethod.Get, "goodses");
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(new QueryGoods { GoodsName = "aaaaa", Limit = 10, Offset = 0 }), Encoding.UTF8, "application/json");
                //request.Content = new StringContent(JsonConvert.SerializeObject(new Person { Name = "桂素伟", Age = 99 }), Encoding.UTF8, "application/json");
                //var values = new List<KeyValuePair<string, string>>();
                //values.Add(new KeyValuePair<string, string>("goodsname", goodsName));
                //values.Add(new KeyValuePair<string, string>("limit", _pagecount.ToString()));
                //values.Add(new KeyValuePair<string, string>("offset", (pageIndex * _pagecount).ToString()));
                //request.Content = new FormUrlEncodedContent(values);
                var response = client.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(content);

                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(content);
                }


            }
        }
    }
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
    public class QueryGoods
    {
        public int Limit { get; set; }

        public int Offset { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
    }
}
