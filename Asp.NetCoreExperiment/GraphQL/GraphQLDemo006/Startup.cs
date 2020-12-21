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

namespace GraphQLDemo06
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

            services.AddSha256DocumentHashProvider();
            var path = $"{Directory.GetCurrentDirectory()}/queries";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
                     
          
            services.AddFileSystemQueryStorage(path);

            

            services.AddPooledDbContextFactory<AdventureWorks2016Context>(
                (services, options) => options
                .UseSqlServer(Configuration.GetConnectionString("ConnectionString"))
                .UseLoggerFactory(services.GetRequiredService<ILoggerFactory>()))
                .AddGraphQLServer()
                .AddQueryType<Query>()
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
            
    
            app.UseEndpoints(endpoints =>
            {                
                endpoints.MapGraphQL();
            });
        }
    }
}
