using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo02
{
    public partial class CreditCard
    {
        public CreditCard()
        {
            PersonCreditCards = new HashSet<PersonCreditCard>();
            SalesOrderHeaders = new HashSet<SalesOrderHeader>();
        }

        public int CreditCardId { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<PersonCreditCard> PersonCreditCards { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
    }
}
