using System;
using System.Collections.Generic;

namespace MiniDemo04.Models
{
    public partial class User
    {
        public User()
        {
            UserExams = new HashSet<UserExam>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }

        public virtual ICollection<UserExam> UserExams { get; set; }
    }
}
