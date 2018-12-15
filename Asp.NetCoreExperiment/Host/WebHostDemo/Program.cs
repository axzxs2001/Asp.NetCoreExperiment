using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace WebHostDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            Console.ReadLine();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //调用 ConfigureKestrel 来重写 CreateDefaultBuilder 在配置 Kestrel 时建立的 30,000,000 字节默认 
            .ConfigureKestrel((context, options) =>
            {
                options.Limits.MaxRequestBodySize = 20000000;
            })
            //捕获启动错误
            .CaptureStartupErrors(true)
            //确定是否应捕获详细错误
            .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
             //设置应用的环境。
            .UseEnvironment(EnvironmentName.Development)
            .UseStartup<Startup>();
    }
}
