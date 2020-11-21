using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo01
{
    public partial class CurrencyRate
    {
        public CurrencyRate()
        {
            SalesOrderHeaders = new HashSet<SalesOrderHeader>();
        }

        public int CurrencyRateId { get; set; }
        public DateTime CurrencyRateDate { get; set; }
        public string FromCurrencyCode { get; set; }
        public string ToCurrencyCode { get; set; }
        public decimal AverageRate { get; set; }
        public decimal EndOfDayRate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Currency FromCurrencyCodeNavigation { get; set; }
        public virtual Currency ToCurrencyCodeNavigation { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
    }
}
