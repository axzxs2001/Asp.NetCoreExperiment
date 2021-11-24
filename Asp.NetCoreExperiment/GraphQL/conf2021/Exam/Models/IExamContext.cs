using Microsoft.EntityFrameworkCore;

namespace Exam.Models
{
    public interface IExamContext
    {
        DbSet<Answer> Answers { get; set; }
        DbSet<ExamPaperQuestion> ExamPaperQuestions { get; set; }
        DbSet<ExamPaper> ExamPapers { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<QuestionType> QuestionTypes { get; set; }
        DbSet<SubjectType> SubjectTypes { get; set; }
        DbSet<UserExamAnswer> UserExamAnswers { get; set; }
        DbSet<UserExam> UserExams { get; set; }
        DbSet<User> Users { get; set; }
    }
}