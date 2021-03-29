using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationWebDemo01
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string IDCard { get; set; }
        public PersonAddress Address { get; set; }
    }
    public class PersonAddress
    {
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
    }
}
