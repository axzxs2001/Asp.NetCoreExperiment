using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo02
{
    public partial class StateProvince
    {
        public StateProvince()
        {
            Addresses = new HashSet<Address>();
            SalesTaxRates = new HashSet<SalesTaxRate>();
        }

        public int StateProvinceId { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }
        public bool? IsOnlyStateProvinceFlag { get; set; }
        public string Name { get; set; }
        public int TerritoryId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual CountryRegion CountryRegionCodeNavigation { get; set; }
        public virtual SalesTerritory Territory { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<SalesTaxRate> SalesTaxRates { get; set; }
    }
}
