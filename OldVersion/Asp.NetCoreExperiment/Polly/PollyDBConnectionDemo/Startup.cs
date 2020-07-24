using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PollyDBConnectionDemo.Services;

namespace PollyDBConnectionDemo
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
            services.AddScoped<AdoNetPolly>();
            services.AddScoped<DapperPolly>();


            services.AddSingleton("Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=postgres;Pooling=true;MinPoolSize=1;MaxPoolSize=100;CommandTimeout=100;");
            services.AddScoped<IDbConnection, Npgsql.NpgsqlConnection>();
            services.AddScoped<ReliableDapper>();
            services.AddControllers()
                .AddNewtonsoftJson();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
