using GreenDonut;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Fetching;
using HotChocolate.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLDemo04
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddDataLoader<PersonDataLoader>()

                .AddFiltering()
                .AddSorting()
                .AddProjections()
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

    /// <summary>
    /// 查询类
    /// </summary>
    public class Query
    {
        [UseProjection]
        public Task<Person> GetPerson(int id, [DataLoader] PersonDataLoader personLoader)
        {

            return personLoader.LoadAsync(id);
        }


    }

    public class PersonDataLoader : DataLoaderBase<int, Person>
    {
        public PersonDataLoader() : base(new BatchScheduler(), new DataLoaderOptions<int>())
        {
        }
        protected override ValueTask<IReadOnlyList<Result<Person>>> FetchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            var list = new List<Result<Person>>();
            for (int i = 0; i < 200; i++)
            {
                var person1 = new Person { Id = i, Tel = "12323232321", Name = "张三" + i };
                var result1 = Result<Person>.Resolve(person1);
                list.Add(result1);
            }
            var result = new ValueTask<IReadOnlyList<Result<Person>>>(list);
            return result;
        }

    }
    /// <summary>
    /// 用户
    /// </summary>
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
    }
}
