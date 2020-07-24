using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KestrelDemo01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel((context, options) =>
                    {
                        //客户端最大连接数
                        options.Limits.MaxConcurrentConnections = 100;
                        //对于已从 HTTP 或 HTTPS 升级到另一个协议（例如，Websocket 请求）的连接，有一个单独的限制。
                        options.Limits.MaxConcurrentUpgradedConnections = 100;
                        //最大连接数不受限制 (NULL)。
                        options.Limits.MaxRequestBodySize = 10 * 1024;
                        //请求正文最小数据速率
                        options.Limits.MinRequestBodyDataRate =
                            new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                        //应答请求正文最小数据速率
                        options.Limits.MinResponseDataRate =
                            new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));

                        options.Listen(IPAddress.Loopback, 5000);
                        options.Listen(IPAddress.Loopback, 5001, listenOptions =>
                        {
                            listenOptions.UseHttps("testCert.pfx", "testPassword");
                        });
                        options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
                        //请求标头超时
                        options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
                        //同步 IO
                        options.AllowSynchronousIO = true;
                        //每个连接的最大流
                        options.Limits.Http2.MaxStreamsPerConnection = 100;
                        //标题表大小
                        options.Limits.Http2.HeaderTableSize = 4096;
                        //最大帧大小
                        options.Limits.Http2.MaxFrameSize = 16384;
                        //最大请求标头大小
                        options.Limits.Http2.MaxRequestHeaderFieldSize = 8192;
                        //初始连接窗口大小
                        options.Limits.Http2.InitialConnectionWindowSize = 131072;
                        //初始流窗口大小
                        options.Limits.Http2.InitialStreamWindowSize = 98304;

                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
