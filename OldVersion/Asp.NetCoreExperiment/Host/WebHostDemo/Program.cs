using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace WebHostDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();
            //通过调用 Start 方法以非阻止方式运行主机
            // webHost.Start();
            //Run 方法启动 Web 应用并阻止调用线程，直到关闭主机
            webHost.Run();  

            Console.ReadLine();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hostsettings.json", optional: true)
                .AddCommandLine(args)
                .Build();


            var hostBuilder = WebHost.CreateDefaultBuilder(args)
               //调用 ConfigureKestrel 来重写 CreateDefaultBuilder 在配置 Kestrel 时建立的 30,000,000 字节默认 
               .ConfigureKestrel((context, options) =>
               {                  
                   options.Limits.MaxRequestBodySize = 20000000;
               })
               .UseConfiguration(config)
               //捕获启动错误
               .CaptureStartupErrors(true)
               //确定是否应捕获详细错误
               .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
               //设置应用的环境。
               .UseEnvironment(EnvironmentName.Development)
               //指定等待 Web 主机关闭的时长。
               .UseShutdownTimeout(TimeSpan.FromSeconds(10))
               //启动Starup所在程序集
               // .UseStartup("StartupAssemblyName")
               .UseStartup<Startup>();
            return hostBuilder;

        }
    }
}
