using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo02
{
    public partial class ShipMethod
    {
        public ShipMethod()
        {
            PurchaseOrderHeaders = new HashSet<PurchaseOrderHeader>();
            SalesOrderHeaders = new HashSet<SalesOrderHeader>();
        }

        public int ShipMethodId { get; set; }
        public string Name { get; set; }
        public decimal ShipBase { get; set; }
        public decimal ShipRate { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
    }
}
