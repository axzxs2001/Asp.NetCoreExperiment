using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo01
{
    public partial class CountryRegion
    {
        public CountryRegion()
        {
            CountryRegionCurrencies = new HashSet<CountryRegionCurrency>();
            SalesTerritories = new HashSet<SalesTerritory>();
            StateProvinces = new HashSet<StateProvince>();
        }

        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<CountryRegionCurrency> CountryRegionCurrencies { get; set; }
        public virtual ICollection<SalesTerritory> SalesTerritories { get; set; }
        public virtual ICollection<StateProvince> StateProvinces { get; set; }
    }
}
