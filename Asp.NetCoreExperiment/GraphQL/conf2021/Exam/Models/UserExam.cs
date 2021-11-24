using System;
using System.Collections.Generic;

namespace Exam.Models
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

        public virtual ExamPaper ExamPapger { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<UserExamAnswer> UserExamAnswers { get; set; }
    }
}
