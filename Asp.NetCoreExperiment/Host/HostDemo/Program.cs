using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                   .ConfigureLogging((hostContext,configLogging) =>
                   {                
                       configLogging.AddConsole();
                       configLogging.SetMinimumLevel(LogLevel.Debug);
                   })
                   .Build();



            await host.RunAsync();
        }
    }
}
