using System;
using System.Collections.Generic;
using System.Linq;
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
            var port = Convert.ToInt32(configuration.GetSection("AppSetting").GetSection("Port").Value);
            var name = configuration.GetSection("AppSetting").GetSection("Name").Value;
            Console.Title = name + "   port:" + port;
            var log = new Logger();
            var config = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + @"\config.txt");
            _node = TcpRaftNode.GetFromConfig(1, config,
                           System.IO.Directory.GetCurrentDirectory() + $@"\DBreeze\{name}", port, log,
                           (entityName, index, data) =>
                           {
                               Console.WriteLine($"{entityName}/{index} { System.Text.Encoding.UTF8.GetString(data)}");
                               return true;
                           });
            _node.Start();
        }

        public void Add(string content)
        {
            Console.WriteLine(content);
            _node.AddLogEntry(System.Text.Encoding.UTF8.GetBytes(content));
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
