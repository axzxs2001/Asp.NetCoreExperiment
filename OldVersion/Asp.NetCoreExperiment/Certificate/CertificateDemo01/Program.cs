using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace CertificateDemo01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 80);
                    options.Listen(IPAddress.Any, 443, listenOptions =>
                    {
                        var signingCertificate = new X509Certificate2("server.pfx", "ssssss");
                        Console.WriteLine("Server.pfx SerialNumber:{0}",signingCertificate.SerialNumber);
                        var httpsConnectionAdapterOptions = new HttpsConnectionAdapterOptions()
                        {

                            ClientCertificateMode = ClientCertificateMode.AllowCertificate,
                            SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                            //验证客户端证书是否正规
                            ClientCertificateValidation = (cer, chain, error) =>
                             {                                 
                                 //var verify = false;
                                 //foreach (X509ChainElement element in chain.ChainElements)
                                 //{
                                 //    Console.WriteLine("SerialNumber:{0}",element.Certificate.SerialNumber);
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
                             },
                            ServerCertificate = signingCertificate
                        };
                        listenOptions.UseHttps(httpsConnectionAdapterOptions);
                    });
                })
                .UseStartup<Startup>();
    }
}


