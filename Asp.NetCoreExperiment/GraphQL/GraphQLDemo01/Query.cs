using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
namespace GraphQLDemo01
{
    public class Query
    {
        [UseDbContext(typeof(AdventureWorks2016Context))]
        [UseFiltering]
        [UseSorting]
        [UseProjection]
        public IQueryable<Product> GetProducts([ScopedService] AdventureWorks2016Context context)
        {
            return context.Products;
        }

        [UseDbContext(typeof(AdventureWorks2016Context))]
        [UseFiltering]
        [UseSorting]
        [UseProjection]
        public IQueryable<Person> GetPersons([ScopedService] AdventureWorks2016Context context)
        {
            return context.People;
        }
    }
}
