using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo02
{
    public partial class VPersonDemographic
    {
        public int BusinessEntityId { get; set; }
        public decimal? TotalPurchaseYtd { get; set; }
        public DateTime? DateFirstPurchase { get; set; }
        public DateTime? BirthDate { get; set; }
        public string MaritalStatus { get; set; }
        public string YearlyIncome { get; set; }
        public string Gender { get; set; }
        public int? TotalChildren { get; set; }
        public int? NumberChildrenAtHome { get; set; }
        public string Education { get; set; }
        public string Occupation { get; set; }
        public bool? HomeOwnerFlag { get; set; }
        public int? NumberCarsOwned { get; set; }
    }
}
