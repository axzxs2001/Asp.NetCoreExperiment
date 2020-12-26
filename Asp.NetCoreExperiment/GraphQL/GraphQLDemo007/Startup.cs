using HotChocolate;
using HotChocolate.Data.Sorting;
using HotChocolate.Types;
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

            services
           .AddRouting()
           .AddGraphQLServer()
           .AddQueryType<Query>();


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

    public class Query
    {
        public Book GetBook() => new Book { Title = "C# in depth", Author = "Jon Skeet" };
    }


    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor
                .Field(f => f.GetBook())
                .Type<BookType>();
        }
    }
 
    public class Book
    {
        public string Title { get; set; }

        public string Author { get; set; }
    }


    public class BookType : ObjectType<Book>
    {     
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
            descriptor
                .Field(f => f.Title)
                .Type<StringType>();

            descriptor
                .Field(f => f.Author)
                .Type<StringType>();
        }
    }

}
