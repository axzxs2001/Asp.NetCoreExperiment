using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo06
{
    public partial class PhoneNumberType
    {
        public PhoneNumberType()
        {
            PersonPhones = new HashSet<PersonPhone>();
        }

        public int PhoneNumberTypeId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<PersonPhone> PersonPhones { get; set; }
    }
}
