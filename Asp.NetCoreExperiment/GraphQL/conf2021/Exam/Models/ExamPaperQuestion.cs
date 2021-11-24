using System;
using System.Collections.Generic;

namespace Exam.Models
{
    public partial class ExamPaperQuestion
    {
        public int Id { get; set; }
        public int ExamPaperId { get; set; }
        public int QuestionId { get; set; }

        public virtual ExamPaper ExamPaper { get; set; } = null!;
        public virtual Question Question { get; set; } = null!;
    }
}
