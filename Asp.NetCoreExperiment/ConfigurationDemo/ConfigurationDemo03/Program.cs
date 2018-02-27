using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationDemo03
{
    class Program
    {
        private static IDisposable callbackRegistration;

        public static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddIniFile("Settings.ini")
                .Build()
                .ReloadOnChanged("Settings.ini");

            callbackRegistration = configuration.GetReloadToken().RegisterChangeCallback(OnSettingChanged, configuration);

            Console.Read();
        }

        private static void OnSettingChanged(object state)
        {
            callbackRegistration?.Dispose();
            IConfiguration configuration = (IConfiguration)state;
            Console.WriteLine(configuration.Get<ThreadPoolSettings>());
            callbackRegistration = configuration.GetReloadToken().RegisterChangeCallback(OnSettingChanged, state);
        }
    }

    public class ThreadPoolSettings
    {
        public int MinThreads { get; set; }
        public int MaxThreads { get; set; }
        public override string ToString()
        {
            return string.Format("Thread pool size: [{0}, {1}]", this.MinThreads, this.MaxThreads);
        }
    }
}
