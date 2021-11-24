using System;
using System.Collections.Generic;

namespace Exam.Models
{
    public partial class Answer
    {
        public Answer()
        {
            UserExamAnswers = new HashSet<UserExamAnswer>();
        }

        public int Id { get; set; }
        public string Sequre { get; set; } = null!;
        public string Answer1 { get; set; } = null!;
        public bool IsTrue { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; } = null!;
        public virtual ICollection<UserExamAnswer> UserExamAnswers { get; set; }
    }
}
