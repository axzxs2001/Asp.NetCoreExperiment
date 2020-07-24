using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskAsyncDemo04
{
    class Program
    {
        public static void Main()
        {

            //GetPageSizeAsync("http://www.baidu.com").Wait(); //等待
            //GetPageSizeAsync("http://www.baidu.com").GetAwaiter().GetResult();  //等待
            GetPageSizeAsync("http://www.baidu.com").GetAwaiter();  //不等待
                                                                    //GetPageSizeAsync("http://www.baidu.com");  //不等待
            Console.WriteLine($"Main: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")}");
            Console.ReadLine();

        }

        private static async Task GetPageSizeAsync(string url)
        {
            var client = new HttpClient();
            var uri = new Uri(Uri.EscapeUriString(url));
            byte[] urlContents = await client.GetByteArrayAsync(uri);
            Console.WriteLine($"GetPageSizeAsync: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")}");
            Console.WriteLine($"{url}: {urlContents.Length / 2:N0} characters");
        }
    }
}
