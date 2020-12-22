using HotChocolate;
using HotChocolate.Data.Sorting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace GraphQLDemo07
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


            services.AddInMemorySubscriptions();

            services
                .AddGraphQLServer()
                .AddQueryType<Subscription>()
                .AddFiltering()
                .AddSorting()
                .AddProjections();

     
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseWebSockets();


            app.UseEndpoints(endpoints =>
            {                
                endpoints.MapGraphQL();
            });
        }
    }
}
