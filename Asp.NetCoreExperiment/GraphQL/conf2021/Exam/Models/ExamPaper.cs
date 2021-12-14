using System;
using System.Collections.Generic;

namespace Exam.Models
{

    public partial class ExamPaper
    {
        public ExamPaper()
        {
            UserExams = new HashSet<UserExam>();
            Questions = new HashSet<Question>();
        }

        [GraphQLDescription("编号")]
        public int Id { get; set; }

        [GraphQLDescription("标题")]
        public string Title { get; set; } = null!;

        [GraphQLDescription("备注")]
        public string? Memo { get; set; }

        [GraphQLDescription("创建时间")]
        public DateTime CreateTime { get; set; }

        [UseFiltering]
        [UseSorting]
        public virtual ICollection<UserExam> UserExams { get; set; }

        [UseFiltering]
        [UseSorting]
        public virtual ICollection<Question> Questions { get; set; }
    }
}
