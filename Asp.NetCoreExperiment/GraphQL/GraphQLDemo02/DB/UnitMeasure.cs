using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo02
{
    public partial class UnitMeasure
    {
        public UnitMeasure()
        {
            BillOfMaterials = new HashSet<BillOfMaterial>();
            ProductSizeUnitMeasureCodeNavigations = new HashSet<Product>();
            ProductVendors = new HashSet<ProductVendor>();
            ProductWeightUnitMeasureCodeNavigations = new HashSet<Product>();
        }

        public string UnitMeasureCode { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<BillOfMaterial> BillOfMaterials { get; set; }
        public virtual ICollection<Product> ProductSizeUnitMeasureCodeNavigations { get; set; }
        public virtual ICollection<ProductVendor> ProductVendors { get; set; }
        public virtual ICollection<Product> ProductWeightUnitMeasureCodeNavigations { get; set; }
    }
}
