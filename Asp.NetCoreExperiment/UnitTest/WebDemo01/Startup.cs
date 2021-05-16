using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebDemo01.Services;

namespace WebDemo01
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
            services.AddControllers();
            //services.AddScoped<IDbConnection, MySqlConnection>();
            //services.AddScoped<IDapperPlus, DapperPlus>();


            //services.AddScoped<IDbConnection, MySqlConnection>();
            //services.AddScoped<IDapperPlusRead, DapperPlusRead>();
            //services.AddScoped<IDapperPlusWrite, DapperPlusWrite>();

            //两种库
            //services.AddScoped<IDbConnection, MySqlConnection>();
            //services.AddScoped<IDbConnection, SqlConnection>();
            //services.AddScoped<IDapperPlus, MySqlDapperPlus>();
            //services.AddScoped<IDapperPlus, MsSqlDapperPlus>();

            //两种库，计写分离
            services.AddScoped<IDbConnection, MySqlConnection>();
            services.AddScoped<IDbConnection, SqlConnection>();
            services.AddScoped<IDapperPlusRead, MySqlDapperPlusRead>();
            services.AddScoped<IDapperPlusRead, MsSqlDapperPlusRead>();
            services.AddScoped<IDapperPlusWrite, MySqlDapperPlusWrite>();
            services.AddScoped<IDapperPlusWrite, MsSqlDapperPlusWrite>();


            services.AddScoped<IShopService, ShopService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
