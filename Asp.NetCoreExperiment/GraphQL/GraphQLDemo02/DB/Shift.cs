using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo02
{
    public partial class Shift
    {
        public Shift()
        {
            EmployeeDepartmentHistories = new HashSet<EmployeeDepartmentHistory>();
        }

        public byte ShiftId { get; set; }
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; }
    }
}
