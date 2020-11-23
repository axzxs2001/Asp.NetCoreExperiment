using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo02
{
    public partial class ProductVendor
    {
        public int ProductId { get; set; }
        public int BusinessEntityId { get; set; }
        public int AverageLeadTime { get; set; }
        public decimal StandardPrice { get; set; }
        public decimal? LastReceiptCost { get; set; }
        public DateTime? LastReceiptDate { get; set; }
        public int MinOrderQty { get; set; }
        public int MaxOrderQty { get; set; }
        public int? OnOrderQty { get; set; }
        public string UnitMeasureCode { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Vendor BusinessEntity { get; set; }
        public virtual Product Product { get; set; }
        public virtual UnitMeasure UnitMeasureCodeNavigation { get; set; }
    }
}
