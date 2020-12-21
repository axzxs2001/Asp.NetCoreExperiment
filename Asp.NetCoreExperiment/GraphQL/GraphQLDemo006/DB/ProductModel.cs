using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo06
{
    public partial class ProductModel
    {
        public ProductModel()
        {
            ProductModelIllustrations = new HashSet<ProductModelIllustration>();
            ProductModelProductDescriptionCultures = new HashSet<ProductModelProductDescriptionCulture>();
            Products = new HashSet<Product>();
        }

        public int ProductModelId { get; set; }
        public string Name { get; set; }
        public string CatalogDescription { get; set; }
        public string Instructions { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProductModelIllustration> ProductModelIllustrations { get; set; }
        public virtual ICollection<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
