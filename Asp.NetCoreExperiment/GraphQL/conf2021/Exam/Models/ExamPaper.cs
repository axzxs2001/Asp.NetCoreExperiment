using System;
using System.Collections.Generic;

namespace Exam.Models
{
    public partial class ExamPaper
    {
        public ExamPaper()
        {
            ExamPaperQuestions = new HashSet<ExamPaperQuestion>();
            UserExams = new HashSet<UserExam>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Memo { get; set; }
        public double TotalScore { get; set; }
        public int QuestionCount { get; set; }

        public DateTime CreateTime { get; set; }
        [UseFiltering]
        [UseSorting]
        public virtual ICollection<ExamPaperQuestion> ExamPaperQuestions { get; set; }
        [UseFiltering]
        [UseSorting]
        public virtual ICollection<UserExam> UserExams { get; set; }
    }
}
