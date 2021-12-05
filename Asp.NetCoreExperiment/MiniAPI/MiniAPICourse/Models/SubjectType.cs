using System;
using System.Collections.Generic;

namespace MiniAPICourse.Models
{
    public partial class SubjectType
    {
        public SubjectType()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
