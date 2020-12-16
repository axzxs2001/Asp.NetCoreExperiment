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

        public static List<Result<Student>> Persons = new List<Result<Student>>();

        public void ConfigureServices(IServiceCollection services)
        {
            for (int i = 0; i < 200; i++)
            {
                var student = new Student { Id = i, Tel = "13453467" + i.ToString("D3"), Name = NameCreater.GetFullName() };
                var result = Result<Student>.Resolve(student);
                Persons.Add(result);
            }

            services
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

    /// <summary>
    /// 查询类
    /// </summary>
    public class Query
    {
        public Task<Student> GetStudent(int id, [DataLoader] StudentDataLoader loader)
        {
            return loader.LoadAsync(id);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class StudentDataLoader : DataLoaderBase<int, Student>
    {
        public StudentDataLoader(IBatchScheduler scheduler) : base(scheduler)
        {
        }
        protected override ValueTask<IReadOnlyList<Result<Student>>> FetchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {

            return new ValueTask<IReadOnlyList<Result<Student>>>(Startup.Persons.Where(s => keys.Contains(s.Value.Id)).ToList());

        }

    }
    /// <summary>
    /// 学生
    /// </summary>
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
    }
}
