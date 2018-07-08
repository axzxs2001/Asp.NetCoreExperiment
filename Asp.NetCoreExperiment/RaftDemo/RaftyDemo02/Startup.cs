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
using Rafty.Concensus.Node;
using Rafty.Concensus.Peers;
using Rafty.FiniteStateMachine;
using Rafty.Infrastructure;
using Rafty.Log;

namespace RaftyDemo02
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            var log = new InMemoryLog();
            var fsm = new InMemoryStateMachine();
            var settings = new InMemorySettings(1000, 3500, 50, 5000);

            var _peers = new List<IPeer>();
            var peer1 = new NodePeer();
         
            _peers.Add(new NodePeer() { });
            var peersProvider = new InMemoryPeersProvider(_peers);
            var node = new Node(fsm, log, settings, peersProvider, loggerFactory);
            node.Start(new NodeId("gsw" + DateTime.Now.ToString("HHmmssfff")));
            
            app.UseMvc();
        }
    }
}
