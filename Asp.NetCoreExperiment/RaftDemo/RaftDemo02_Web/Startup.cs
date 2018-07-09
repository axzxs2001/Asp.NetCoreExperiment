using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Raft;
using Raft.Transport;

namespace RaftDemo02_Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<RaftOpt>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }

    public class RaftOpt
    {
        TcpRaftNode _node;
        public RaftOpt(IConfiguration configuration)
        {
            var appSetting = configuration.GetSection("AppSetting");
            var port = Convert.ToInt32(appSetting.GetSection("Port").Value);
            var name = appSetting.GetSection("Name").Value;
            Console.Title = name + "   port:" + port;
            var log = new Logger();
            var config = File.ReadAllText(Directory.GetCurrentDirectory() + @"\config.txt");
            _node = TcpRaftNode.GetFromConfig(1, config,
                           Directory.GetCurrentDirectory() + $@"\DBreeze\{name}", port, log,
                           (entityName, index, data) =>
                           {
                               Console.WriteLine($"{entityName}/{index} { Encoding.UTF8.GetString(data)}");
                               return true;
                           });
            _node.Start();
        }

        public void Add(string content)
        {
            Console.WriteLine(content);
            _node.AddLogEntry(Encoding.UTF8.GetBytes(content));
        }
    }

    public class Logger : IWarningLog
    {
        public void Log(WarningLogEntry logEntry)
        {
            Console.WriteLine(logEntry.ToString());
        }
    }
}
