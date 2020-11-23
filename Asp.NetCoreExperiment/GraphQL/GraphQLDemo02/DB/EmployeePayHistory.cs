using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo02
{
    public partial class EmployeePayHistory
    {
        public int BusinessEntityId { get; set; }
        public DateTime RateChangeDate { get; set; }
        public decimal Rate { get; set; }
        public byte PayFrequency { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Employee BusinessEntity { get; set; }
    }
}
