using System;
using System.Collections.Generic;

namespace Exam.Models
{
    public partial class QuestionType
    {
        public QuestionType()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; } = null!;

        public virtual ICollection<Question> Questions { get; set; }
    }
}
