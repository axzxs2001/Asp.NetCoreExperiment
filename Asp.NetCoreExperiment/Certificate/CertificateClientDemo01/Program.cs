using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CertificateClientDemo01
{
    class Program
    {
        static void Main(string[] args)
        {


            try
            {
                var url = "";
                //创建post请求                
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                //var path = AppDomain.CurrentDomain.BaseDirectory;
                //X509Certificate2 x509Certificate2 = new X509Certificate2(@"C:\Users\NSS\source\repos\OrigamiTest\OrigamiTest\netstarcert.pfx", "123456");//导入证书

                //x509Certificate2.CopyWithPrivateKey(RSA.Create());
                //handler.ClientCertificates.Add(x509Certificate2);//添加证书      

                var cerFile = "";
                var keyFile = "";
                var cert = new X509Certificate2(cerFile);
                cert.PrivateKey = CreateRSAFromFile(keyFile);
                handler.ClientCertificates.Add(cert);

               var httpClient = new HttpClient(handler);
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;//使用Tls1.2协议
                httpClient.BaseAddress = new Uri(url);//域名
                httpClient.DefaultRequestHeaders.Accept.Clear();
                var content = new StringContent("", Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync("/order", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var back = response.Content.ReadAsStringAsync().Result;


                }

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        static RSACryptoServiceProvider CreateRSAFromFile(string filename)
        {
            byte[] pvk = null;
            using (var fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                pvk = new byte[fs.Length];
                fs.Read(pvk, 0, pvk.Length);
            }

            var rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(pvk);

            return rsa;
        }
    }
}
