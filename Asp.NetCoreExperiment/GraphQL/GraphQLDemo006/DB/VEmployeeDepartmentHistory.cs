using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo06
{
    public partial class VEmployeeDepartmentHistory
    {
        public int BusinessEntityId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Shift { get; set; }
        public string Department { get; set; }
        public string GroupName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
