using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo06
{
    public partial class Vendor
    {
        public Vendor()
        {
            ProductVendors = new HashSet<ProductVendor>();
            PurchaseOrderHeaders = new HashSet<PurchaseOrderHeader>();
        }

        public int BusinessEntityId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public byte CreditRating { get; set; }
        public bool? PreferredVendorStatus { get; set; }
        public bool? ActiveFlag { get; set; }
        public string PurchasingWebServiceUrl { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual BusinessEntity BusinessEntity { get; set; }
        public virtual ICollection<ProductVendor> ProductVendors { get; set; }
        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
    }
}
