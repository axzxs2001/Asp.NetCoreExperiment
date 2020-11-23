using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo02
{
    public partial class Store
    {
        public Store()
        {
            Customers = new HashSet<Customer>();
        }

        public int BusinessEntityId { get; set; }
        public string Name { get; set; }
        public int? SalesPersonId { get; set; }
        public string Demographics { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual BusinessEntity BusinessEntity { get; set; }
        public virtual SalesPerson SalesPerson { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
