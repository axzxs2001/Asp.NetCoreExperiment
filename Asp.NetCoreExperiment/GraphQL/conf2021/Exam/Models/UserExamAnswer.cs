using System;
using System.Collections.Generic;

namespace Exam.Models
{
    public partial class UserExamAnswer
    {
        public int Id { get; set; }
        public int UserExamId { get; set; }
        public int AnswerId { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual Answer Answer { get; set; } = null!;
        public virtual UserExam UserExam { get; set; } = null!;
    }
}
