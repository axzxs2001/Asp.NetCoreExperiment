using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo02
{
    public partial class ProductSubcategory
    {
        public ProductSubcategory()
        {
            Products = new HashSet<Product>();
        }

        public int ProductSubcategoryId { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
