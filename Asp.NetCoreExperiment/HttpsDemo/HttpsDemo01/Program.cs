using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HttpsDemo01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                //添加.pfx证书方式，debian和win10测试成功
                .UseKestrel(kestrelOptions =>
                {
                    kestrelOptions.Listen(IPAddress.Any, 5000);
                    kestrelOptions.Listen(IPAddress.Any, 5001, listenOptions =>
                    {
                        listenOptions.UseHttps("证书名称.pfx", "证书密码");
                    });
                });
    }
}
