using System;
using System.Collections.Generic;

namespace MiniAPICourse.Models
{
    public partial class UserExam
    {
        public UserExam()
        {
            UserExamAnswers = new HashSet<UserExamAnswer>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExamPapgerId { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual ExamPaper ExamPapger { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserExamAnswer> UserExamAnswers { get; set; }
    }
}
