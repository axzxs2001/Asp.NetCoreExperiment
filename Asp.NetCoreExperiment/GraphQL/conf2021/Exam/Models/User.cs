using System;
using System.Collections.Generic;

namespace Exam.Models
{
    public partial class User
    {
        public User()
        {
            UserExams = new HashSet<UserExam>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Tel { get; set; }

        public virtual ICollection<UserExam> UserExams { get; set; }
    }
}
