using System;
using System.IO;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace CertificateClientDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("回车开始");
            while (true)
            {
                try
                {
                    Console.WriteLine("1、双向认证Https请求   2、Http请求 ");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            HttpsMethod();
                            break;
                        case "2":
                            HttpMethod();                                     
                            break;
                    }
                    void HttpsMethod()
                    {
                        var handler = new HttpClientHandler();
                        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                        handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls;
                        try
                        {
                            var path = Directory.GetCurrentDirectory() + "\\server.pfx";
                            var crt = new X509Certificate2(path, "111111");
                            handler.ClientCertificates.Add(crt);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        //验证服务器证书是否正规
                        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                        {                         
                            var res = chain.Build(cert);
                            Console.WriteLine($"*********  验证服务端证书 chain.Build={res}");
                            return true;
                        };
                        var client = new HttpClient(handler);
                        var url = "https://127.0.0.1/api/values";
                        var response = client.GetAsync(url).Result;
                        Console.WriteLine(response.IsSuccessStatusCode);
                        var back = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(back);
                    }
                    void HttpMethod()
                    {
                        var client = new HttpClient();
                        var url = "http://127.0.0.1/api/values";// 
                        var response = client.GetAsync(url).Result;
                        Console.WriteLine(response.IsSuccessStatusCode);
                        var back = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(back);
                    }                  

                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.InnerException?.InnerException?.Message);
                }
            }

        }
    }
}
