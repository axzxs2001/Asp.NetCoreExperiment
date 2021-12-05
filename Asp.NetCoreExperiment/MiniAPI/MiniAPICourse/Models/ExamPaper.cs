using System;
using System.Collections.Generic;

namespace MiniAPICourse.Models
{
    public partial class ExamPaper
    {
        public ExamPaper()
        {
            UserExams = new HashSet<UserExam>();
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Memo { get; set; }
        public double TotalScore { get; set; }
        public int QuestionCount { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual ICollection<UserExam> UserExams { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
