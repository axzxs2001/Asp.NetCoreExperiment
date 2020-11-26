using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphQLDemo03_Grades
{
    public class Startup
    {
     
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IGradeRepository, GradeRepository>() 
                .AddGraphQLServer()  
                .AddQueryType<Query>()
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