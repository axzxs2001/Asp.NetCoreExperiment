using System.Linq;
using Dapper;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace GraphQLDemo01
{
    public class Query
    {
        [UseDbContext(typeof(AdventureWorks2016Context))]
        [UseOffsetPaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Product> GetProducts([ScopedService] AdventureWorks2016Context context)
        {
            //dapper方式
            //using (var con = new SqlConnection("Data Source=.;Initial Catalog=AdventureWorks2016;Persist Security Info=True;User ID=sa;Password=sa"))
            //{
              
            //    return con.Query<Product>("select * from production.product");
            //}
            //ef方式
            return context.Products;
        }

        [UseDbContext(typeof(AdventureWorks2016Context))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Person> GetPersons([ScopedService] AdventureWorks2016Context context)
        {
            return context.People;
        }
    }
}
