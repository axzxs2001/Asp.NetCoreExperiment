using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphQLDemo00
{
    public class Startup
    {
       public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGraphQLServer()//引入GraphQL           
                .AddQueryType<Query>()//注入查询类型      
                .AddProjections()//映射字段
                .AddFiltering()//注入查询过滤器
                .AddSorting()//注入排序
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