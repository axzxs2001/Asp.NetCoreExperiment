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
                        var signingCertificate = new X509Certificate2("server.pfx", "111111");
                        var httpsConnectionAdapterOptions = new HttpsConnectionAdapterOptions()
                        {

                            ClientCertificateMode = ClientCertificateMode.RequireCertificate,//AllowCertificate,
                            SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                            //验证客户器证书是否正规
                            ClientCertificateValidation = (cer, chain, error) =>
                             {
                                 var res = chain.Build(cer);
                                 Console.WriteLine($"*********  验证客户端证书 chain.Build={res}");
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


