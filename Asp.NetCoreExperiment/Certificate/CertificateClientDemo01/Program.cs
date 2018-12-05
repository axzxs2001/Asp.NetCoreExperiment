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
            Console.WriteLine("enter start");
            while (true)
            {
                try
                {
                    Console.WriteLine("1、Https   2、Http");
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
                        handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls | SslProtocols.None | SslProtocols.Tls11;
                        try
                        {
                            var crt = new X509Certificate2(Directory.GetCurrentDirectory() + "/client.pfx", "cccccc");
                            handler.ClientCertificates.Add(crt);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        //验证服务器证书是否正规
                        handler.ServerCertificateCustomValidationCallback = (message, cer, chain, errors) =>
                        {
                            //var verify = false;
                            //foreach (X509ChainElement element in chain.ChainElements)
                            //{
                            //    Console.WriteLine("SerialNumber:{0}", element.Certificate.SerialNumber);
                            //    Console.WriteLine("Element subject name: {0}", element.Certificate.Subject);
                            //    Console.WriteLine("Element issuer name: {0}", element.Certificate.Issuer);
                            //    Console.WriteLine("Element certificate valid until: {0}", element.Certificate.NotAfter);
                            //    Console.WriteLine("Element certificate is valid: {0}", element.Certificate.Verify());
                            //    Console.WriteLine("Element error status length: {0}", element.ChainElementStatus.Length);
                            //    Console.WriteLine("Element information: {0}", element.Information);
                            //    Console.WriteLine("Number of element extensions: {0}{1}", element.Certificate.Extensions.Count, Environment.NewLine);
                            //    if (element.Certificate.Issuer == cer.Issuer)
                            //    {
                            //        verify = element.Certificate.Verify();
                            //    }
                            //}
                            //Console.WriteLine($"*********   X509Certificate2.Verify={cer.Verify()}");
                            //Console.WriteLine($"*********   Element certificate is valid: {verify}");
                            var res = chain.Build(cer);                         
                            Console.WriteLine($"*********   X509Chain.Build={res}");
                            return res;
                        };
                        var client = new HttpClient(handler);
                        var url = "https://192.168.252.41/api/values";
                        var response = client.GetAsync(url).Result;
                        Console.WriteLine(response.IsSuccessStatusCode);
                        var back = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(back);
                    }
                    void HttpMethod()
                    {
                        var client = new HttpClient();
                        var url = "http://192.168.252.41/api/values";// 
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
