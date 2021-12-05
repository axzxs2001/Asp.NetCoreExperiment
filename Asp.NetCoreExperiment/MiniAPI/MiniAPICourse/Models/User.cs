using System;
using System.Collections.Generic;

namespace MiniAPICourse.Models
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
        public byte[] Tel { get; set; }

        public virtual ICollection<UserExam> UserExams { get; set; }
    }
}
