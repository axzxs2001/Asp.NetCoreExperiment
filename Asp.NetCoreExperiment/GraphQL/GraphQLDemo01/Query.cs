using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
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
