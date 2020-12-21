using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo06
{
    public partial class PurchaseOrderDetail
    {
        public int PurchaseOrderId { get; set; }
        public int PurchaseOrderDetailId { get; set; }
        public DateTime DueDate { get; set; }
        public short OrderQty { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal RejectedQty { get; set; }
        public decimal StockedQty { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual PurchaseOrderHeader PurchaseOrder { get; set; }
    }
}
