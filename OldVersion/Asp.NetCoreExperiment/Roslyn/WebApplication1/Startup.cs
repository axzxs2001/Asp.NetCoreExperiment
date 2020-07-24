using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace WebApplication1
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
            var basePath = System.IO.Directory.GetCurrentDirectory();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1", Contact = new OpenApiContact { Email = "", Name = "WebApplication1" }, Description = "WebApplication1 Details" });
                var xmlPath = Path.Combine(basePath, "WebApplication1.xml");
                options.IncludeXmlComments(xmlPath, true);
                options.DocInclusionPredicate((docName, description) => true);

            });
        
            // services.AddMvc(opt => opt.EnableEndpointRouting = false);
            services.AddControllers();
            services.AddDyanmicController();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "APIDemo01";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIDemo01");
            });
            app.UseRouting();

            app.UseAuthorization();
        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });   
        }
    }

    public class ScheduleService : IDynamicController
    {
        [HttpPost]
        public ApiResultModel<ScheduleModel> Add(ScheduleModel schedule)
        {
            return new ApiResultModel<ScheduleModel>() { Result = schedule };
        }
    }
    public class ScheduleModel
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string EventName { get; set; }

        public string Content { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
    }
    public class ApiResultModel<TResult>
    {
        public TResult Result { get; set; }
    }
}
