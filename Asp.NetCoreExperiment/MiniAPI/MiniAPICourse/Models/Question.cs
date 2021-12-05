using System;
using System.Collections.Generic;

namespace MiniAPICourse.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            ExamPapers = new HashSet<ExamPaper>();
        }

        public int Id { get; set; }
        public string Question1 { get; set; }
        public double Score { get; set; }
        public int QuestionTypeId { get; set; }
        public int SujectTypeId { get; set; }

        public virtual QuestionType QuestionType { get; set; }
        public virtual SubjectType SujectType { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<ExamPaper> ExamPapers { get; set; }
    }
}
