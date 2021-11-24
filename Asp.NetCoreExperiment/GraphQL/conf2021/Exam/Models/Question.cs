using System;
using System.Collections.Generic;

namespace Exam.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            ExamPaperQuestions = new HashSet<ExamPaperQuestion>();
        }

        public int Id { get; set; }
        public string Question1 { get; set; } = null!;
        public double Score { get; set; }
        public int QuestionTypeId { get; set; }
        public int SujectTypeId { get; set; }

        public virtual QuestionType QuestionType { get; set; } = null!;
        public virtual SubjectType SujectType { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<ExamPaperQuestion> ExamPaperQuestions { get; set; }
    }
}
