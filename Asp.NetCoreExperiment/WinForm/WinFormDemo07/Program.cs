using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace WinFormDemo07
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();


            var host = CreateHostBuilder().Build();

            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<MainForm>());

            //Application.Run(new MainForm());
        }

        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {

            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.SetMinimumLevel(LogLevel.Information);
                        loggingBuilder.AddNLog("nlog.config");
                    });
                  
                    services.AddTransient<MainForm>();
                    services.AddTransient<Child01Form>();
                    services.AddTransient<Child02Form>();
                    services.AddTransient<IDataService, DataService>();
                });

        }
    }
    public interface IDataService
    {
        bool ModifyData(string name);
    }
    public class DataService : IDataService
    {
        public bool ModifyData(string name)
        {
            return false;
        }
    }
}