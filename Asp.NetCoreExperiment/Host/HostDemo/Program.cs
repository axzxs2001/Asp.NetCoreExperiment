using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HostDemo
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var host = new HostBuilder()
                   .ConfigureServices((hostContext, services) =>
                   {
                       services.Configure<HostOptions>(option =>
                       {
                           option.ShutdownTimeout = System.TimeSpan.FromSeconds(20);
                       });
                   })
                .Build();

            await host.RunAsync();
        }
    }
}
