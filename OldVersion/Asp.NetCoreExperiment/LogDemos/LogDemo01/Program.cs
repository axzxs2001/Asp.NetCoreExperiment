using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace LogDemo01
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // NLog: setup the logger first to catch all errors
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            //    LogManager.Setup().SetupSerialization(s =>
            //s.RegisterObjectTransformation<Exception>(ex => new
            //{
            //    Type = ex.GetType().ToString(),
            //    Message =ex.Message.Replace("\r\n",""),
            //    StackTrace = ex.StackTrace?.Replace("\r\n", ""),
            //    Source = ex.Source?.Replace("\r\n", ""),
            //    InnerException = ex.InnerException,
            //    Status = 200,
            //    Response = "111111111",
            //}));



            try
            {
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors

                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

        }

        /// <summary>
        /// 构建webhost
        /// </summary>
        /// <param name="args">启动参数</param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseUrls("http://*:5000")
            .ConfigureLogging(a =>
            {
                a.ClearProviders();
               
            })
            .UseStartup<Startup>()
            .UseNLog() // NLog: setup NLog for Dependency injectio
            .Build();
    }
}
