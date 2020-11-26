using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Language;

namespace GraphQLDemo03_gateway
{
    public class Startup
    {
        public const string Students = "students";
        public const string Grades = "grades";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient(Students, c => c.BaseAddress = new Uri("http://localhost:7000/graphql"));
            services.AddHttpClient(Grades, c => c.BaseAddress = new Uri("http://localhost:9000/graphql"));
            services
              .AddGraphQLServer()        
              .AddRemoteSchema(Students, ignoreRootTypes: true)
              .AddRemoteSchema(Grades, ignoreRootTypes: true)
              .AddTypeExtensionsFromString("type Query { }")
              .AddTypeExtensionsFromFile("StudentStitching.graphql")
              .AddTypeExtensionsFromFile("GradeStitching.graphql")
              .AddTypeExtensionsFromFile("StudentExtendStitching.graphql")
              .AddTypeExtensionsFromFile("GradeExtendStitching.graphql")
              ;
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
