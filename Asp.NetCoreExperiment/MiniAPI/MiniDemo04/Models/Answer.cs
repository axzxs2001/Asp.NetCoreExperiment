using System;
using System.Collections.Generic;

namespace MiniDemo04.Models
{
    public partial class Answer
    {
        public Answer()
        {
            UserExamAnswers = new HashSet<UserExamAnswer>();
        }

        public int Id { get; set; }
        public string Sequre { get; set; }
        public string Answer1 { get; set; }
        public bool IsTrue { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<UserExamAnswer> UserExamAnswers { get; set; }
    }
}
